using System;
using System.Windows.Forms;
using static ProjectOne.TomodachiLife;

namespace LoginNameSpace
{
    public partial class Login : Form
    {
        public string Server => ipAddressText.Text;
        public int Port => int.TryParse(portText.Text, out int port) ? port : 0;
        public string Username => usernameText.Text;
        public string Password => passwordText.Text;

        public Login()
        {
            InitializeComponent();
        }
        public Login(string server, int port, string username, string password) : this()
        {
            ipAddressText.Text = server;
            portText.Text = port.ToString();
            usernameText.Text = username;
            passwordText.Text = password;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
