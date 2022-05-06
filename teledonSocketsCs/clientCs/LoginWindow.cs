using System;
using System.Windows.Forms;

namespace clientCs
{
    public partial class LoginWindow : Form
    {
        private TeledonClientController controller;
        public LoginWindow(TeledonClientController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
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