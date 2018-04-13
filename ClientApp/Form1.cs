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
            if (textBox1.Text.Length > 0)
            {
                try
                {
                    Connect.connect(textBox1.Text);
                    ConsoleBox.Text += "Подключено" + '\n';
                    Connect.getRSA();
                    ConsoleBox.Text += "Ключ RSA получен" + '\n';
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    ConsoleBox.Text += "Соединение не установлено , повторите попытку позже." + '\n';
                }
            }
            else
            {
                ConsoleBox.Text += "Введите IP Сервера";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
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

            if (Connect.PathFile != null)
            {
                d.encrypt("2345", Connect.PathFile);
                ConsoleBox.Text += "Ключевое слово и файл зашифрованы DES" + '\n';
                f = true;
            }
            else
            {
                ConsoleBox.Text += "Добавьте файл" + '\n';
            }
            if (Connect.RsaKey != 0 && d.decodeKey != null)
            {
                Connect.sendDES(d);
                ConsoleBox.Text += "Ключ DES отправлен" + '\n';
            }
            else
            {
                ConsoleBox.Text += "Для отправки ключа DES вы должны получить RSA ключ" + '\n';
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Connect.PathFile != null)
            {
                d.encrypt("2345", Connect.PathFile);
                ConsoleBox.Text += "Ключевое слово и файл зашифрованы DES" + '\n';
                f = true;
            }
            else
            {
                ConsoleBox.Text += "Добавьте файл" + '\n';
            }
            if (Connect.RsaKey != 0 && d.decodeKey != null)
            {
                Connect.sendDES(d);
                ConsoleBox.Text += "Ключ DES отправлен" + '\n';
            }
            else
            {
                ConsoleBox.Text += "Для отправки ключа DES вы должны получить RSA ключ" + '\n';
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            Connect.openDecrypt();
        }
    }
}
