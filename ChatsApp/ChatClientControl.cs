using ChatObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatsApp
{
    public class ChatClientControl
    {
        private Socket socketSession = null;
        private NetworkStream networkStream = null;


        public bool isConnected()
        {
            return this.socketSession != null && this.socketSession.IsBound;
        }

        public bool connect(string hostAddress, int iPort)
        {
            bool result = false;

            try
            {
                this.socketSession = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.socketSession.SendTimeout = 0;
                this.socketSession.ReceiveTimeout = 0;
                this.socketSession.Connect(hostAddress, iPort);
                this.networkStream = new NetworkStream(this.socketSession);
                result = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public void sendDataObject(Object o)
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                binaryFormatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                binaryFormatter.Serialize(this.networkStream, o);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Object recvDataObject()
        {
            Object o = null;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                o = binaryFormatter.Deserialize(this.networkStream);
            }
            catch (Exception e)
            {
                throw e;
            }
            return o;
        }

        public void close()
        {
            if (this.networkStream != null)
            {
                this.networkStream.Close();
                this.networkStream = null;
            }
            if (this.socketSession != null)
            {
                this.socketSession.Close();
                this.socketSession = null;
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
