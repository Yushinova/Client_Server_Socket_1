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

namespace Client
{
    public partial class Form1 : Form
    {
        public Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        public static IPAddress ip = IPAddress.Parse("127.0.0.1");
        public IPEndPoint ep = new IPEndPoint(ip, 1024);
        string message = string.Empty;
        public Form1()
        {
            InitializeComponent();
            Task.Run(Go);
        }
        private void Go()
        {
            try
            {
                s.Connect(ep);

                    while (s.Connected)
                    {
                        byte[] buffer = new byte[1024];
                        int l = s.Receive(buffer);
                        message = Encoding.UTF8.GetString(buffer, 0, l);
                        //LabelTime.Text = message;
                        Invoke(new Action(() => LabelTime.Text = message));
                    }
            }
            catch { }

        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            s.Close();
        }
    }
}
