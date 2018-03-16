using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsoleBox.Text += "Подключение..." + '\n';
            host = Dns.GetHostEntry("127.0.0.1");
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
            socket.Send(BitConverter.GetBytes(1));
                      
        }
    }
}
