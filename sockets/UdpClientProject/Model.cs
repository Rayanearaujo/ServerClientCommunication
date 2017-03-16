using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;

namespace UdpClientProject
{
    public class Model : INotifyPropertyChanged
    {

        private UInt32 _remotePort = 5000;
        private string _remoteIPAddress = "127.0.0.1";

        private UdpClient _dataSocket;
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
                myStatusMessage = myStatusMessage + ": " + "ERROR: Message not sent\n";
                return;
            }
            myStatusMessage = myStatusMessage + DateTime.Now + " Message Sucefully Sent \n";
        }


    }
}
