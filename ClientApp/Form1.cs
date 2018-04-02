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
        DES d = new DES();
        bool f = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Connect.connect();
                ConsoleBox.Text += "Подключено" + '\n';
            }
            catch(System.Net.Sockets.SocketException ex)
            {
                ConsoleBox.Text += "Соединение не установлено , повторите попытку позже." + '\n';
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Connect.getRSA();
                ConsoleBox.Text += "Ключ RSA получен" + '\n';
            }
            catch(System.Net.Sockets.SocketException ex)
            {
                ConsoleBox.Text += "Соединение не установлено, нажмите ПОДКЛЮЧИТЬСЯ" + '\n';
            }
            catch(System.NullReferenceException ex)
            {
                ConsoleBox.Text += "Соединение не установлено, нажмите ПОДКЛЮЧИТЬСЯ" + '\n';
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Connect.RsaKey != 0 && d.decodeKey!=null)
            {
                Connect.sendDES(d);
                ConsoleBox.Text += "Ключ DES отправлен" + '\n';
            }
            else
            {
                ConsoleBox.Text += "Для отправки ключа DES вы должны получить RSA ключ и ввести пароль для DES" + '\n';
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            using (op)
            {
                if (op.ShowDialog() == DialogResult.OK)
                {
                    Connect.PathFile = op.FileName;
                }
            }
            ConsoleBox.Text += "Открыт файл " + Connect.PathFile + '\n';
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (encodeKeyTextBox.Text.Length > 0 && Connect.PathFile != null)
            {
                d.encrypt(encodeKeyTextBox.Text, Connect.PathFile);
                ConsoleBox.Text += "Ключевое слово и файл зашифрованы DES" + '\n';
                f = true;
            }
            else
            {
                ConsoleBox.Text += "Добавьте файл и ключевое слово" + '\n';
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (f)
            {
                Connect.sendFile();
                ConsoleBox.Text += "Файл отпрвален" + '\n';
            }
            else
            {
                ConsoleBox.Text += "Сначала добавьте файл и зашифруйте его DES" + '\n';
            }
           
        }
    }
}
