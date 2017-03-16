using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyUdpServer
{
    class Model : INotifyPropertyChanged
    {

        private UInt32 _localPort = 5000;
        private string _localIPAddress = "127.0.0.1";
        private Thread _receivedDataThread;
        UdpClient _dataSocket;
        bool isServerThreadOn = true;

        public Model()
        {
        }

        public void InitModel()
        {
            _dataSocket = new UdpClient((int)_localPort);
            ThreadStart threadFunction = new ThreadStart(ReceiveThreadFromClient);
            _receivedDataThread = new Thread(threadFunction);
            _receivedDataThread.Start();
        }

        public void ReceiveThreadFromClient()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_localIPAddress), (int)_localPort);

            while (true)
            {
                try
                {
                    Byte[] receivedData = _dataSocket.Receive(ref endPoint);
                    receivedMessage = receivedMessage + DateTime.Now + ": " + System.Text.Encoding.Default.GetString(receivedData) + "\n";
                }
                catch (SocketException ex)
                {
                    receivedMessage = receivedMessage + " message not received properly! \n";
                }
            }
        }

        public void CleanUp()
        {
            isServerThreadOn = false;
            _receivedDataThread.Abort();
            _dataSocket.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _receivedMessage;
        public string receivedMessage
        {
            get { return _receivedMessage; }
            set
            {
                _receivedMessage = value;
                OnPropertyChanged("receivedMessage");
            }
        }
       
    }
}
