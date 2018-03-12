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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ip = host.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ip, 18000);
            Socket socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);
        }
    }
}
