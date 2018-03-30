using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class Form1 : Form
    {
        IPHostEntry host;
        IPAddress ip;
        IPEndPoint endPoint;
        Socket socket;
        byte[] buf = new byte[1000000];
        BigInteger rsaKey = 0;
        string pathFile = null;
        DES d = new DES();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            host = Dns.GetHostEntry("localhost");
            ip = host.AddressList[0];
            endPoint = new IPEndPoint(ip, 18000);
            socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);
            if (socket.Connected)
            {
                ConsoleBox.Text = "Подключился" + '\n';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {  
            socket.Send(Encoding.Default.GetBytes("Запрос RSA"));


            int received = socket.Receive(buf);
            MemoryStream s = new MemoryStream();
            s.Write(buf, 0, received);
            rsaKey = BigInteger.Parse(Encoding.Default.GetString(s.ToArray()));
            ConsoleBox.Text += "Ключ получен";
            MessageBox.Show("RSA ключ получен");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rsaKey != 0 && d.decodeKey!=null)
            {
                var CipherKey = RSA.encryption(Encoding.Default.GetBytes(d.decodeKey), rsaKey);
                MemoryStream s = new MemoryStream();
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(s, CipherKey);
                socket.Send(s.ToArray());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            using (op)
            {
                if (op.ShowDialog() == DialogResult.OK)
                {
                    pathFile = op.FileName;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (encodeKeyTextBox.Text.Length > 0 && pathFile != null)
            {
                d.encrypt(encodeKeyTextBox.Text, pathFile);
               // decodeKeyTextBox.Text = d.decodeKey;

            }
            else
            {
                MessageBox.Show("Добавьте файл и введите ключевое слово");
            }
        }
    }
}
