using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

namespace UdpClientProject
{
    public class Model : INotifyPropertyChanged
    {
        //this is the thread that will run in the background waiting for incomming data
        private Thread _receivedDataThread;

        private UInt32 _localPort = 5001;
        private UInt32 _remotePort = 5000;
        private string _remoteIPAddress = "127.0.0.1";

        private UdpClient _dataSocket, _receive_dataSocket;
        private string message;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string _myMessage;
        public string myMessage
        {
            get { return _myMessage; }
            set
            {
                _myMessage = value;
                OnPropertyChanged("myMessage");
            }
        }

        private string _myStatusMessage;
        public string myStatusMessage
        {
            get { return _myStatusMessage; }
            set
            {
                _myStatusMessage = value;
                OnPropertyChanged("myStatusMessage");
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

        public Model()
        {
        }

        public void InitModel()
        {
            _dataSocket = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_remoteIPAddress), (int)_remotePort);
            message = myMessage;
            Byte[] sendBytes = Encoding.ASCII.GetBytes(message);

            try
            {

                _dataSocket.Send(sendBytes, sendBytes.Length, endPoint);
            }
            catch (SocketException ex)
            {
                myStatusMessage = myStatusMessage + ": " + "ERRO: Mensagem nao enviada\n";
                return;
            }
            myStatusMessage = myStatusMessage + DateTime.Now + ": " + "Mensagem enviada com sucesso! \n";


            _receive_dataSocket = new UdpClient((int)_localPort);
            ThreadStart threadFunction = new ThreadStart(ReceiveThreadFunction);
            _receivedDataThread = new Thread(threadFunction);
            _receivedDataThread.Start();
        }

        public void ReceiveThreadFunction()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_remoteIPAddress), (int)_localPort);
            while (true)
            {
                try
                {
                    //wait for data
                    Byte[] receiveData = _receive_dataSocket.Receive(ref endPoint);

                    //convert byte array to a string
                    Console.Write(System.Text.Encoding.Default.GetString(receiveData));
                    receivedMessage = DateTime.Now + ": " + 
                        System.Text.Encoding.Default.GetString(receiveData);
                }
                catch (SocketException ex)
                {
                    Console.Write("problems receiving message from server");
                }
            }
        }

    }
}
