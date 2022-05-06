using System.ComponentModel;

namespace clientCs
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.dataGridViewCharitableCase = new System.Windows.Forms.DataGridView();
            this.dataGridViewDonors = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxFindDonor = new System.Windows.Forms.TextBox();
            this.buttonSearchDonor = new System.Windows.Forms.Button();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxPhoneNr = new System.Windows.Forms.TextBox();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxAmountDonated = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewCharitableCase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewDonors)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCharitableCase
            // 
            this.dataGridViewCharitableCase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCharitableCase.Location = new System.Drawing.Point(27, 28);
            this.dataGridViewCharitableCase.Name = "dataGridViewCharitableCase";
            this.dataGridViewCharitableCase.RowHeadersWidth = 51;
            this.dataGridViewCharitableCase.RowTemplate.Height = 24;
            this.dataGridViewCharitableCase.Size = new System.Drawing.Size(456, 219);
            this.dataGridViewCharitableCase.TabIndex = 2;
            // 
            // dataGridViewDonors
            // 
            this.dataGridViewDonors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDonors.Location = new System.Drawing.Point(416, 353);
            this.dataGridViewDonors.Name = "dataGridViewDonors";
            this.dataGridViewDonors.RowHeadersWidth = 51;
            this.dataGridViewDonors.RowTemplate.Height = 24;
            this.dataGridViewDonors.Size = new System.Drawing.Size(575, 219);
            this.dataGridViewDonors.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(889, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 63);
            this.button1.TabIndex = 4;
            this.button1.Text = "Log out";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxFindDonor
            // 
            this.textBoxFindDonor.Location = new System.Drawing.Point(483, 287);
            this.textBoxFindDonor.Name = "textBoxFindDonor";
            this.textBoxFindDonor.Size = new System.Drawing.Size(211, 22);
            this.textBoxFindDonor.TabIndex = 5;
            // 
            // buttonSearchDonor
            // 
            this.buttonSearchDonor.Location = new System.Drawing.Point(740, 267);
            this.buttonSearchDonor.Name = "buttonSearchDonor";
            this.buttonSearchDonor.Size = new System.Drawing.Size(155, 62);
            this.buttonSearchDonor.TabIndex = 6;
            this.buttonSearchDonor.Text = "Search donor";
            this.buttonSearchDonor.UseVisualStyleBackColor = true;
            this.buttonSearchDonor.Click += new System.EventHandler(this.buttonSearchDonor_Click);
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(161, 299);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(191, 22);
            this.textBoxFirstName.TabIndex = 7;
            // 
            // textBoxPhoneNr
            // 
            this.textBoxPhoneNr.Location = new System.Drawing.Point(161, 498);
            this.textBoxPhoneNr.Name = "textBoxPhoneNr";
            this.textBoxPhoneNr.Size = new System.Drawing.Size(191, 22);
            this.textBoxPhoneNr.TabIndex = 8;
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(161, 430);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(191, 22);
            this.textBoxAddress.TabIndex = 9;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(161, 362);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(191, 22);
            this.textBoxLastName.TabIndex = 10;
            // 
            // textBoxAmountDonated
            // 
            this.textBoxAmountDonated.Location = new System.Drawing.Point(161, 561);
            this.textBoxAmountDonated.Name = "textBoxAmountDonated";
            this.textBoxAmountDonated.Size = new System.Drawing.Size(191, 22);
            this.textBoxAmountDonated.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(27, 298);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "First name";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(27, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Last name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(27, 429);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 14;
            this.label3.Text = "Address";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 501);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "Phone number";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(27, 560);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Amount donated";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(195, 597);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 45);
            this.button2.TabIndex = 17;
            this.button2.Text = "Save donation";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 654);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAmountDonated);
            this.Controls.Add(this.textBoxLastName);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.textBoxPhoneNr);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.buttonSearchDonor);
            this.Controls.Add(this.textBoxFindDonor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridViewDonors);
            this.Controls.Add(this.dataGridViewCharitableCase);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewCharitableCase)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.dataGridViewDonors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxPhoneNr;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxAmountDonated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.TextBox textBoxFindDonor;
        private System.Windows.Forms.Button buttonSearchDonor;

        private System.Windows.Forms.DataGridView dataGridViewCharitableCase;
        private System.Windows.Forms.DataGridView dataGridViewDonors;
        private System.Windows.Forms.Button button1;

        #endregion
    }
}