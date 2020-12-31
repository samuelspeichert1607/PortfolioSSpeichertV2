using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.IO;

namespace CardsAgainstHumanity
{
    public partial class FormConnection : Form
    {
        //TcpClient clientSocket = new TcpClient();
        public FormConnection()
        {
            InitializeComponent();
            

        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            FormPrincipal partie = new FormPrincipal("1" + textBoxUsername.Text + "," + textBoxPassword.Text, textBoxUsername.Text, textBoxPassword.Text, textBoxIP.Text, textBoxPort.Text);
        }
    }
}                                                                                                                                                                
