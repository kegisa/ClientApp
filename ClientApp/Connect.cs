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
using System.Windows.Forms;

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

        /// <summary>
        /// 
        /// </summary>
        public static void sendFile()
        {
            socket.Send(File.ReadAllBytes("C:\\Users\\user\\Documents\\out1.txt")); 
            //socket.Send(File.ReadAllBytes("C:\\Users\\user\\Documents\\out1.txt")); 
            /*  FileStream file1 = new FileStream("C:\\Users\\user\\Documents\\out1.txt", FileMode.Open);
              StreamReader reader = new StreamReader(file1);
             using (reader)
              {
                  char[] c = null;
                  while (reader.Peek() >= 0)
                  {
                      c = new char[10];
                      reader.ReadBlock(c, 0, c.Length);
                      byte[] mass = Encoding.UTF32.GetBytes(c);
                      socket.Send(mass);
                  }
              }*/
            /* byte[] bytes = new byte[55];
             var stream = File.OpenRead("C:\\Users\\user\\Documents\\out1.txt");
             while (stream.CanSeek)
             {
                 int count = stream.Read(bytes, 0, 55);
                 socket.Send(bytes);
                 stream.Seek(55, SeekOrigin.Begin);
             }*/
            /* byte[] bytes = new byte[100];
             using (var stream = File.OpenRead("C:\\Users\\user\\Documents\\out1.txt"))
             {
                 int length = (int)stream.Length;
                 for (int i = 0; i * 100 <= length;i++)
                 {

                         int count = stream.Read(bytes, i * 100, 100);
                         socket.Send(bytes);

                 }

             }*/


        }

    }
}
