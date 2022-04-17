using clientCs;
using System;
using System.Windows.Forms;

namespace client
{
    public partial class LoginWindow : Form
    {
        private TeledonClientController controller;
        public LoginWindow(TeledonClientController ctrl)
        {
            InitializeComponent();
            this.controller = ctrl;
        }

        private void buttonLogin_Click(object sender, System.EventArgs e)
        {
            String username = textBoxUsername.Text;
            String password = textBoxPassword.Text;
            try
            {
                controller.login(username, password);
                MainWindow mainWindow = new MainWindow(controller);
                mainWindow.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Login error " + ex.Message);
            }
        }
    }
}