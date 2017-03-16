using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyUdpClient
{
    class Model : INotifyPropertyChanged
    {
        
        private UInt32 _remotePort = 5000;
        private string _remoteIPAddress = "127.0.0.1";
        private UdpClient _dataSocket;
        private string messageContent;

        public Model()
        {
        }

        public void InitModel()
        {
            _dataSocket = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_remoteIPAddress), (int)_remotePort);
            messageContent = messageInput;

            Byte[] sendBytes = Encoding.ASCII.GetBytes(messageContent);

            try
            {
                _dataSocket.Send(sendBytes, sendBytes.Length, endPoint);
            }
            catch (SocketException ex)
            {
                messageStatus = messageStatus + ": " + "ERROR: your message was not sent\n";
                return;
            }
            messageStatus = messageStatus + DateTime.Now + "Your message was Sent! \n";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        private string _messageInput;
        public string messageInput
        {
            get { return _messageInput; }
            set
            {
                _messageInput = value;
                OnPropertyChanged("messageInput");
            }
        }

        private string _messageStatus;
        public string messageStatus
        {
            get { return _messageStatus; }
            set
            {
                _messageStatus = value;
                OnPropertyChanged("messageStatus");
            }
        }


    }
}
