using System;
using System.Windows.Forms;

namespace clientCs
{
    public partial class MainWindow : Form
    {
        private TeledonClientController Controller;
        public MainWindow(TeledonClientController controller)
        {
            InitializeComponent();
            this.Controller = controller;
        }

        private void button1_Click(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Log out...");
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Controller.logout();
                Controller.updateEvent -= volunteerUpdate;
                Application.Exit();
            }
        }

        public void volunteerUpdate(object sender, TeledonUserEventArgs e)
        {
            if (e.UserEventType == TeledonUserEvent.NEW_DONATION)
            {
                
            }
        }

        private void buttonSearchDonor_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}