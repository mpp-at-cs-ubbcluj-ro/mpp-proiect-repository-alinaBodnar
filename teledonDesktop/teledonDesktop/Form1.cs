using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using teledonCS.model;
using teledonCS.repository;
using teledonDesktop.services;

namespace teledonDesktop
{
    public partial class Form1 : Form
    {
        private VolunteerService _volunteerService;
        private CharitableCaseService _charitableCaseService;
        private DonationService _donationService;
        private DonorService _donorService;
        public Form1()
        {
            InitializeComponent();
        }

        public void setServices(VolunteerService volunteerService,CharitableCaseService charitableCaseService,DonationService donationService,DonorService donorService)
        {
            this._volunteerService = volunteerService;
            this._charitableCaseService = charitableCaseService;
            this._donationService = donationService;
            this._donorService = donorService;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text != "" && textBoxPassword.Text != "")
            {
                validateLogin();
            }
            else
            {
                MessageBox.Show("Please enter username and password");
            }
           
        }

        private void validateLogin()
        {
            String username = textBoxUsername.Text;
            String password = textBoxPassword.Text;
            Volunteer volunteer =  _volunteerService.getVolunteer(username, password);
            if (volunteer != null)
            {
                loadCharitableCase();
                
            }
            else
            {
                MessageBox.Show("This volunteer doesn't exist!");
            }
        }

        private void loadCharitableCase()
        {
            try
            {
                Form2 form2 = new Form2(_charitableCaseService,_donationService,_donorService);
                form2.setServices(_charitableCaseService, _donationService, _donorService);
                form2.setForm1(this);
                form2.Show();
                this.Hide();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't load new window");
            }
        }
    }
}