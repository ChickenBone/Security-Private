using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

public class TCPClient
    {
        private TcpClient socketConnection;
        private Thread clientReceiveThread;
        public bool connected = false;
        public void Start()
        {
        try
        {
            ConnectToTcpServer();
        }
        catch { }
        }
        private void ConnectToTcpServer()
        {
            try
            {
                clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
            }
            catch
            {
            }
        }
        private void ListenForData()
        {
            try
            {
            while (!connected)
            {
                try
                {
                    socketConnection = new TcpClient(Voyager.Entry.IP, Voyager.Entry.Port);
                    connected = true;
                }
                catch
                {
                    Debug.WriteLine("Error Connecting to server retrying in 3 seconds");
                    Thread.Sleep(3000);
                    connected = false;
                }
            }
            Byte[] bytes = new Byte[1024];
                while (true)
                {
                    using (NetworkStream stream = socketConnection.GetStream())
                    {
                        int length;
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            string serverMessage1 = Encoding.ASCII.GetString(incommingData);
                            string serverMessage = Voyager.Crypto.Decrypt(serverMessage1);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void SendMessage(string message)
        {
            if (socketConnection == null)
            {
            }
            try
            {
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    string clientMessage = message;
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(Voyager.Crypto.Encrypt(clientMessage));
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                }
            }
            catch
            {
            }
        }
    }
