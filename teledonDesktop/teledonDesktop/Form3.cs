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
using teledonDesktop.services;

namespace teledonDesktop
{
    public partial class Form3 : Form
    {
        private CharitableCaseService _charitableCaseService;
        private DonationService _donationService;
        private DonorService _donorService;
        private CharitableCase charitableCase;
        private List<Donor> donorsList = new List<Donor>();
        public Form3(CharitableCaseService charitableCaseService,DonorService donorService,DonationService donationService,CharitableCase charitableCase)
        {
            this._charitableCaseService = charitableCaseService;
            this._donationService = donationService;
            this._donorService = donorService;
            this.charitableCase = charitableCase;
                 
            InitializeComponent();
        }

        private void buttonSaveDonation_Click(object sender, EventArgs e)
        {
            String firstName = textBoxFirstName.Text;
            String lastName = textBoxLastName.Text;
            String address = textBoxAddress.Text;
            String phoneNr = textBoxPhoneNr.Text;
            int amount = Convert.ToInt32(textBoxAmount.Text);

            Donor found = _donorService.getDonorByFullName(firstName, lastName);
            if(found == null)
            {
                _donorService.saveDonor(firstName, lastName, address, phoneNr);
                int idDonor = _donorService.getDonor(firstName, lastName);
                Donor donor = _donorService.getDonorById(idDonor);

                int charitableCaseId = _charitableCaseService.getCharitableCaseByName(charitableCase.name);
                charitableCase.id = charitableCaseId;

                _donationService.saveDonation(charitableCase, donor, amount);

                textBoxFirstName.Clear();
                textBoxLastName.Clear();
                textBoxAddress.Clear();
                textBoxPhoneNr.Clear();
                textBoxAmount.Clear();

                labelInfo.Text = "Successfully added donation";
               


            }
            else
            {

                int charitableCaseId = _charitableCaseService.getCharitableCaseByName(charitableCase.name);
                charitableCase.id = charitableCaseId;

                _donationService.saveDonation(charitableCase, found, amount);

                textBoxFirstName.Clear();
                textBoxLastName.Clear();
                textBoxAddress.Clear();
                textBoxPhoneNr.Clear();
                textBoxAmount.Clear();

                labelInfo.Text = "Successfully added donation";


            }

        }

   

        private void buttonSearchDonor_Click(object sender, EventArgs e)
        {
            String name = textBox1.Text;
            if (name != "")
            {
                List<Donor> donors = _donorService.getAllDonorsByName(name);
                donorsList = donors;
                if (donorsList.Count == 0)
                {
                    MessageBox.Show("No donors found");
                }
                else
                {
                    dataGridViewDonors.DataSource = donorsList;
                }
                
            }
            else
            {
                MessageBox.Show("You must to enter a name");
            }
           
        }

 

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var donor = dataGridViewDonors.SelectedRows[0].DataBoundItem as Donor;
            textBoxFirstName.Text = donor.firstName;
            textBoxLastName.Text = donor.lastName;
            textBoxAddress.Text = donor.address;
            textBoxPhoneNr.Text = donor.phoneNumber;

        }
    }
}
