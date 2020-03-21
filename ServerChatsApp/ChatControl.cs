using ChatObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerChatsApp
{
    public class ChatServerControl
    {
        private TcpListener serverListener = null;

        public bool open(IPAddress iPAddress, int iPort)
        {
            bool result = false;
            try
            {
                this.serverListener = new TcpListener(iPAddress, iPort);
                this.serverListener.Start();
                result = true;
            } 
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        public bool isConnected()
        {
            return this.serverListener != null && this.serverListener.Server.IsBound;
        }

        public Socket acceptChannel()
        {
            return this.serverListener.AcceptSocket();
        }

        public void close()
        {
            if (this.serverListener != null)
            {
                this.serverListener.Stop();
            }
        }
    }

    public abstract class SocketChannelControl
    {
        private Socket socketChannel = null;
        private NetworkStream channelStream = null;
        private Queue mqRequestQueue = null;

        public void startChannel(Socket socketChannel)
        {
            this.socketChannel = socketChannel;
            this.channelStream = new NetworkStream(socketChannel);
            this.mqRequestQueue = new Queue();

            ThreadStart threadStart = new ThreadStart(loadChannel);
            Thread thread = new Thread(threadStart);

            thread.IsBackground = true;
            thread.Start();

            threadStart = new ThreadStart(sendChannel);
            thread = new Thread(threadStart);

            thread.IsBackground = true;
            thread.Start();
        }

        public void sendData(Object o)
        {
            this.mqRequestQueue.Enqueue(o);
        }

        public abstract void eventRecvData(Object o);

        private void loadChannel()
        {
            while (this.socketChannel != null && this.socketChannel.Connected)
            {
                try
                {
                    Object o = recvDataObject();
                    eventRecvData(o);
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                System.Threading.Thread.Sleep(200);
            }
        }

        public void sendChannel()
        {
            while (this.socketChannel != null && this.socketChannel.Connected)
            {
                try
                {
                    Object o = this.mqRequestQueue.Dequeue();
                    if (o != null)
                    {
                        sendDataObject(o);
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
                System.Threading.Thread.Sleep(200);
            }
        }

        public void sendDataObject(Object o)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
            binaryFormatter.Serialize(this.channelStream, o);
        }

        public Object recvDataObject()
        {
            Object o = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
            o = binaryFormatter.Deserialize(this.channelStream);
            return o;
        }

        public Object recvDataObject(Socket socketChannel)
        {
            Object o = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
            o = binaryFormatter.Deserialize(new NetworkStream(socketChannel));
            return o;
        }

        public void stopChannel()
        {
            if (this.channelStream != null)
            {
                this.channelStream.Close();
                this.channelStream = null;
            }

            if (this.socketChannel != null)
            {
                this.socketChannel.Close();
                this.socketChannel = null;
            }
        }
    }

    sealed class AllowAllAssemblyVersionsDeserializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;
            typeToDeserialize = Type.GetType(String.Format("{0}, {1}",
            typeName, assemblyName));

            return typeToDeserialize;
        }
    }
}
