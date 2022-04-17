
namespace client
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewCharitableCases = new System.Windows.Forms.DataGridView();
            this.dataGridViewDonors = new System.Windows.Forms.DataGridView();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearchDonor = new System.Windows.Forms.TextBox();
            this.buttonSearchDonor = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxPhoneNr = new System.Windows.Forms.TextBox();
            this.textBoxAmountDonated = new System.Windows.Forms.TextBox();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.buttonSaveDonor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharitableCases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDonors)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCharitableCases
            // 
            this.dataGridViewCharitableCases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCharitableCases.Location = new System.Drawing.Point(54, 25);
            this.dataGridViewCharitableCases.Name = "dataGridViewCharitableCases";
            this.dataGridViewCharitableCases.RowHeadersWidth = 51;
            this.dataGridViewCharitableCases.RowTemplate.Height = 29;
            this.dataGridViewCharitableCases.Size = new System.Drawing.Size(462, 188);
            this.dataGridViewCharitableCases.TabIndex = 0;
            // 
            // dataGridViewDonors
            // 
            this.dataGridViewDonors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDonors.Location = new System.Drawing.Point(598, 253);
            this.dataGridViewDonors.Name = "dataGridViewDonors";
            this.dataGridViewDonors.RowHeadersWidth = 51;
            this.dataGridViewDonors.RowTemplate.Height = 29;
            this.dataGridViewDonors.Size = new System.Drawing.Size(672, 341);
            this.dataGridViewDonors.TabIndex = 1;
            this.dataGridViewDonors.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDonors_CellContentClick);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Location = new System.Drawing.Point(1155, 25);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(115, 50);
            this.buttonLogout.TabIndex = 2;
            this.buttonLogout.Text = "Log out";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "First name";
            // 
            // textBoxSearchDonor
            // 
            this.textBoxSearchDonor.Location = new System.Drawing.Point(740, 195);
            this.textBoxSearchDonor.Name = "textBoxSearchDonor";
            this.textBoxSearchDonor.Size = new System.Drawing.Size(146, 27);
            this.textBoxSearchDonor.TabIndex = 4;
            // 
            // buttonSearchDonor
            // 
            this.buttonSearchDonor.Location = new System.Drawing.Point(984, 185);
            this.buttonSearchDonor.Name = "buttonSearchDonor";
            this.buttonSearchDonor.Size = new System.Drawing.Size(123, 47);
            this.buttonSearchDonor.TabIndex = 5;
            this.buttonSearchDonor.Text = "Search donor";
            this.buttonSearchDonor.UseVisualStyleBackColor = true;
            this.buttonSearchDonor.Click += new System.EventHandler(this.buttonSearchDonor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Last name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 537);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Amount donated";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 477);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Phone number";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 405);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Address";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(220, 275);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(125, 27);
            this.textBoxFirstName.TabIndex = 10;
            // 
            // textBoxPhoneNr
            // 
            this.textBoxPhoneNr.Location = new System.Drawing.Point(220, 474);
            this.textBoxPhoneNr.Name = "textBoxPhoneNr";
            this.textBoxPhoneNr.Size = new System.Drawing.Size(125, 27);
            this.textBoxPhoneNr.TabIndex = 11;
            this.textBoxPhoneNr.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBoxAmountDonated
            // 
            this.textBoxAmountDonated.Location = new System.Drawing.Point(220, 537);
            this.textBoxAmountDonated.Name = "textBoxAmountDonated";
            this.textBoxAmountDonated.Size = new System.Drawing.Size(125, 27);
            this.textBoxAmountDonated.TabIndex = 12;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(220, 398);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(125, 27);
            this.textBoxAddress.TabIndex = 13;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(220, 337);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(125, 27);
            this.textBoxLastName.TabIndex = 14;
            // 
            // buttonSaveDonor
            // 
            this.buttonSaveDonor.Location = new System.Drawing.Point(117, 593);
            this.buttonSaveDonor.Name = "buttonSaveDonor";
            this.buttonSaveDonor.Size = new System.Drawing.Size(191, 50);
            this.buttonSaveDonor.TabIndex = 15;
            this.buttonSaveDonor.Text = "Save donation";
            this.buttonSaveDonor.UseVisualStyleBackColor = true;
            this.buttonSaveDonor.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 676);
            this.Controls.Add(this.buttonSaveDonor);
            this.Controls.Add(this.textBoxLastName);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.textBoxAmountDonated);
            this.Controls.Add(this.textBoxPhoneNr);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSearchDonor);
            this.Controls.Add(this.textBoxSearchDonor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLogout);
            this.Controls.Add(this.dataGridViewDonors);
            this.Controls.Add(this.dataGridViewCharitableCases);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharitableCases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDonors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCharitableCases;
        private System.Windows.Forms.DataGridView dataGridViewDonors;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSearchDonor;
        private System.Windows.Forms.Button buttonSearchDonor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxPhoneNr;
        private System.Windows.Forms.TextBox textBoxAmountDonated;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Button buttonSaveDonor;
    }
}