using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;


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
        public static byte[] readFile;
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
        static BigInteger n;
        static BigInteger d;
        static BigInteger E;
        public static BigInteger N
        {
            get
            {
                return n;
            }
        }
        static byte[] buf = new byte[100000000];

        public static void connect(String ipText)
        {
            IPHostEntry host = Dns.GetHostEntry(ipText);
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
            E = BigInteger.Parse(Encoding.Default.GetString(s.ToArray()));
            s.Close();

           /* received = socket.Receive(buf);
            MemoryStream s1 = new MemoryStream();
            s1.Write(buf, 0, received);
            d = BigInteger.Parse(Encoding.Default.GetString(s1.ToArray()));
            s.Close();*/

            received = socket.Receive(buf);
            MemoryStream s2 = new MemoryStream();
            s2.Write(buf, 0, received);
            n = BigInteger.Parse(Encoding.Default.GetString(s2.ToArray()));



        }

        public static void sendDES(DES d)
        {
            var CipherKey = RSA.Encryption(Encoding.UTF32.GetBytes(d.decodeKey),E,n);
            MemoryStream s = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, CipherKey);
            socket.Send(s.ToArray());
        }


        public static void sendFile()
        {
            RSA.Generate_keys();
            String OpenKeyE = RSA.GetOpenKey()[0].ToString();
            socket.Send(Encoding.UTF32.GetBytes(OpenKeyE));
            Thread.Sleep(10);

            String OpenKeyN = RSA.GetOpenKey()[1].ToString();
            socket.Send(Encoding.UTF32.GetBytes(OpenKeyN));
            Thread.Sleep(10);

            readFile = File.ReadAllBytes("C:\\Users\\user\\Documents\\out1.txt");
            var Signature = RSA.EncryptSignature(DigitalSignatur.GetHash());

            MemoryStream s = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, Signature);
            socket.Send(s.ToArray());
            Thread.Sleep(10);


            socket.Send(readFile); 
            
        }
        public static void openDecrypt()
        {
            socket.Send(Encoding.Default.GetBytes("Откыть файл"));
        }

    }
}
