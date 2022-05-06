using clientCs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using networking;
using teledonCS.model;

namespace client
{
    public partial class MainWindow : Form
    {
        private readonly TeledonClientController controller;
        private List<CharitableCase> cases = new List<CharitableCase>();
        public MainWindow(TeledonClientController ctrl)
        {
            InitializeComponent();
            this.controller = ctrl;
            initCharitableCases();
            ctrl.updateEvent += userUpdate;
        }

        private void initCharitableCases()
        {
            cases = controller.getCharitableCases();
            dataGridViewCharitableCases.DataSource = cases;
            dataGridViewCharitableCases.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void userUpdate(object sender, TeledonUserEventArgs e)
        {
            if (e.UserEventType == TeledonUserEvent.NEW_DONATION)
            {
              
                List<CharitableCase> casesUpdate = controller.getCharitableCases();
                dataGridViewCharitableCases.BeginInvoke(new UpdateListBoxCallBack(this.updateDataGrid),
                    new Object[] {this.dataGridViewCharitableCases, casesUpdate});
                // dataGridViewCharitableCases.Invoke((Action)delegate { dataGridViewCharitableCases.DataSource = casesUpdate; });
            }
            
        }

        private void updateDataGrid(DataGridView table, List<CharitableCase> charitableCases)
        {
            table.DataSource = null;
            table.DataSource = charitableCases;
        }

        public delegate void UpdateListBoxCallBack(DataGridView table, List<CharitableCase> cases);

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            controller.logout();
            controller.updateEvent -= userUpdate;
            Application.Exit();

        }

        private void buttonSearchDonor_Click(object sender, EventArgs e)
        {
            String nameDonor = textBoxSearchDonor.Text;
            if (nameDonor != "")
            {
                List<Donor> foundDonors = controller.getDonorsByName(nameDonor);
                if (foundDonors.Count == 0)
                {
                    MessageBox.Show("No donors found");
                    dataGridViewDonors.DataSource = null;
                }
                else
                {
                    dataGridViewDonors.DataSource = foundDonors;
                    dataGridViewDonors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                
            }
            else
            {
                MessageBox.Show("You must to enter a name");
            }
          

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // private CharitableCase getCase()
        // {
        //     CharitableCase charitableCaseSelected = dataGridViewCharitableCases.SelectedRows[0].DataBoundItem as CharitableCase;
        //     return charitableCaseSelected;
        // }
        private void button1_Click(object sender, EventArgs e)
        {
            String firstName = textBoxFirstName.Text;
            String lastName = textBoxLastName.Text;
            String address = textBoxAddress.Text;
            String phoneNr = textBoxPhoneNr.Text;
            int amountDonated;
            if (textBoxAmountDonated.Text != "")
            {
                amountDonated = Convert.ToInt32(textBoxAmountDonated.Text);

                if (dataGridViewCharitableCases.SelectedRows.Count == 0)
                {
                    MessageBox.Show("You must to select a charitable case!");

                }
                else
                {
                    CharitableCase charitableCase =
                        dataGridViewCharitableCases.SelectedRows[0].DataBoundItem as CharitableCase;
                    int charitableCaseId = controller.getCharitableCaseByName(charitableCase.name);
                    charitableCase.id = charitableCaseId;
                    if (firstName != "" && lastName != "" && address != "" && phoneNr != "")
                    {
                        Donor foundDonor = controller.getDonorByFullName(firstName, lastName);
                        if (foundDonor == null)
                        {
                            controller.saveDonor(firstName, lastName, address, phoneNr);
                            int idNewDonor = controller.getDonor(firstName, lastName);

                            Donor donor = controller.getDonorById(idNewDonor);

                            Donation newDonation = new Donation(donor, charitableCase, amountDonated);
                            controller.saveDonation(newDonation);

                            textBoxFirstName.Clear();
                            textBoxLastName.Clear();
                            textBoxAddress.Clear();
                            textBoxPhoneNr.Clear();
                            textBoxAmountDonated.Clear();

                            MessageBox.Show("Successufully added donation");


                        }
                        else
                        {
                            Donation newDonation = new Donation(foundDonor, charitableCase, amountDonated);
                            controller.saveDonation(newDonation);

                            textBoxFirstName.Clear();
                            textBoxLastName.Clear();
                            textBoxAddress.Clear();
                            textBoxPhoneNr.Clear();
                            textBoxAmountDonated.Clear();

                            MessageBox.Show("Successufully added donation");

                        }
                    }
                    else
                    {
                        MessageBox.Show("You must to fill the field!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid input");
                return;
            }
            // CharitableCase charitableCase = getCase();

            
            

        }

        private void dataGridViewDonors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDonors.Rows[e.RowIndex];
                textBoxFirstName.Text = row.Cells[1].Value.ToString();
                textBoxLastName.Text = row.Cells[2].Value.ToString();
                textBoxAddress.Text = row.Cells[3].Value.ToString();
                textBoxPhoneNr.Text = row.Cells[4].Value.ToString();
            }

        }
        
    }
}
