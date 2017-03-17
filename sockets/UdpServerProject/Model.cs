﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Net.Sockets;
using System.Net;

using System.Threading;
namespace UdpServerProject
{
    public class Model : INotifyPropertyChanged
    {

        private UInt32 _localPort = 5000;
        private UInt32 _remotePort = 5001;
        private string _localIPAddress = "127.0.0.1";
        
        //this is the thread that will run in the background waiting for incomming data
        private Thread _receivedDataThread;

        //this is the UDP socket that will be used to communicate over the network
        UdpClient _dataSocket;

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

        public Model()
        {
        }

        public void InitModel()
        {
            _dataSocket = new UdpClient((int)_localPort);

            ThreadStart threadFunction = new ThreadStart(ReceiveThreadFunction);
            _receivedDataThread = new Thread(threadFunction);
            _receivedDataThread.Start();
        }

        public void ReceiveThreadFunction()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_localIPAddress), (int)_localPort);
            while (true)
            {
                try
                {
                    //wait for data
                    Byte[] receiveData = _dataSocket.Receive(ref endPoint);

                    //convert byte array to a string
                    myMessage += DateTime.Now + ": "+System.Text.Encoding.Default.GetString(receiveData) + "\n";
                    sendMessageBack();
                }
                catch (SocketException ex)
                {

                }
            }
        }

        private void sendMessageBack()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(_localIPAddress), (int)_remotePort);
            String message1 = "Oi cliente";
            Byte[] sendBytes = Encoding.ASCII.GetBytes(message1);

            try
            {
                _dataSocket.Send(sendBytes, sendBytes.Length, endPoint);
            }
            catch (SocketException ex)
            {
                Console.Write("problems sending message to client");
            }
        }

        public void CleanUp()
        {
            _receivedDataThread.Abort();
            _dataSocket.Close();
        }
    }
}
