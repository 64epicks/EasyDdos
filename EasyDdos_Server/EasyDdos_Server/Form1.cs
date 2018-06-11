using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Threading;

namespace EasyDdos_Server
{
    /*
     * REMINDER:
     * ONLY USE THIS APPLICATION ON SYSTEMS YOU OWN
     * OR SYSTEMS YOU GOT WRITTEN APPROVAL FROM THE OWNER
     */
    public partial class Form1 : Form
    {
        static string SERVER_IP = GetPublicIP();
        private static IPAddress localAdd;
        private static TcpListener listener;

        static string ip;
        static int port;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text += "REMINDER: ONLY USE THIS APPLICATION ON SYSTEMS YOU OWN \n OR SYSTEMS YOU GOT WRITTEN APPROVAL FROM THE OWNER \n" + "Your ip: " + SERVER_IP + "\n";
        }
        static void initCon()
        {
            localAdd = IPAddress.Parse(SERVER_IP);
            listener = new TcpListener(localAdd, 54545);
            listener.Start();

        }
        static void listen()
        {
            
        }
        public static string GetPublicIP()
        {
            string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];
            return a4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text)){
                ip = textBox2.Text;
                port = Int32.Parse(textBox3.Text);
                start();
            }
            else
            {
                MessageBox.Show("Please enter all values");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                for(int currentPort = 1; currentPort <= 65536; currentPort++)
                {
                    if(portTest(textBox2.Text, currentPort))
                    {
                        richTextBox1.Text = "Open port found:" + currentPort + "\n";
                        textBox3.Text = currentPort.ToString();
                        richTextBox1.Text = "";
                        break;
                    }
                    else
                    {
                        richTextBox1.Text = "Testing ports " + currentPort + "/65536\n";
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter an ip adress");
            }
        }
        static bool portTest(string ip, int port)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect(ip, port);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        static void start()
        {

        }
    }
}
