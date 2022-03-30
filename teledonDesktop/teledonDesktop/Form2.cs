using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using teledonCS.model;
using teledonCS.utils.observer;
using teledonCS.utils.observer.events;
using teledonDesktop.services;

namespace teledonDesktop
{
    public partial class Form2 : Form, Observer<DonationEvent>
    {

        private CharitableCaseService _charitableCaseService;
        private DonationService _donationService;
        private DonorService _donorService;
        private List<CharitableCase> cases = new List<CharitableCase>();
        private Form1 form1;
        public Form2(CharitableCaseService charitableCaseService,DonationService donationService,DonorService donorService)
        
        {
  
            this._charitableCaseService = charitableCaseService;
            this._donationService = donationService;
            this._donorService = donorService;
            this.cases = getCases();
            InitializeComponent();
        }

        public void setForm1(Form1 form)
        {
            this.form1 = form;
        }
        private List<CharitableCase> getCases()
        {
            return _charitableCaseService.getAll();
        }
        private void button1_Click(object sender, EventArgs e)
        {
             this.Close();
             form1.Show();

        }

        public void setServices(CharitableCaseService charitableCaseService, DonationService donationService, DonorService donorService)
        {
            this._charitableCaseService = charitableCaseService;
            this._donationService = donationService;
            this._donorService = donorService;
        }

        private void init()
        {
            cases = getCases();
            dataGridViewCharitableCase.DataSource = cases;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

            init();

        }

        public void update(DonationEvent e)
        {
            init();
        }

        private CharitableCase getCase()
        {
            CharitableCase charitableCase = dataGridViewCharitableCase.SelectedRows[0].DataBoundItem as CharitableCase;
            return new CharitableCase(charitableCase.name,charitableCase.amountRaised);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CharitableCase charitableCase = getCase();
                if(charitableCase != null)
                {
                    Form3 form3 = new Form3(_charitableCaseService, _donorService, _donationService,charitableCase);
                    // form3.setCharitableCase(charitableCase);

                    labelInfo.Text = "";
                    form3.Show();
                }
                else
                {
                    labelInfo.Text = "You must to select a charitable case!";

                }
               
                
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}