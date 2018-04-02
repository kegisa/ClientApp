using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Connect
    {
        private static Socket socket;
        public static Socket gSocket
        {
            get
            {
                return socket;
            }
        }
        static string pathFile;
        public static string PathFile
        {
            get
            {
                return pathFile;
            }
            set
            {
                pathFile = value;
            }
        }
        static BigInteger rsaKey = 0;
        public static BigInteger RsaKey
        {
            get
            {
                return rsaKey;
            }
        }
        static byte[] buf = new byte[1000000];

        public static void connect()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ip, 18000);
            socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);
        }

        public static void getRSA() 
        {
            socket.Send(Encoding.Default.GetBytes("Запрос RSA"));
            int received = socket.Receive(buf);
            MemoryStream s = new MemoryStream();
            s.Write(buf, 0, received);
            rsaKey = BigInteger.Parse(Encoding.Default.GetString(s.ToArray()));
        }

        public static void sendDES(DES d)
        {
            var CipherKey = RSA.encryption(Encoding.UTF32.GetBytes(d.decodeKey), rsaKey);
            MemoryStream s = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, CipherKey);
            socket.Send(s.ToArray());
        }

        public static void sendFile()
        {
            socket.Send(File.ReadAllBytes("C:\\Users\\Victor\\Documents\\out1.txt")); 
        }

    }
}
