namespace SimpleInjectorWindowForm1
{
    partial class Form1
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
            this.GetCustomerByIdentifierButton = new System.Windows.Forms.Button();
            this.CustomerListButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetCustomerByIdentifierButton
            // 
            this.GetCustomerByIdentifierButton.Location = new System.Drawing.Point(12, 24);
            this.GetCustomerByIdentifierButton.Name = "GetCustomerByIdentifierButton";
            this.GetCustomerByIdentifierButton.Size = new System.Drawing.Size(250, 23);
            this.GetCustomerByIdentifierButton.TabIndex = 0;
            this.GetCustomerByIdentifierButton.Text = "Customer by Id";
            this.GetCustomerByIdentifierButton.UseVisualStyleBackColor = true;
            this.GetCustomerByIdentifierButton.Click += new System.EventHandler(this.GetCustomerByIdentifierButton_Click);
            // 
            // CustomerListButton
            // 
            this.CustomerListButton.Location = new System.Drawing.Point(12, 53);
            this.CustomerListButton.Name = "CustomerListButton";
            this.CustomerListButton.Size = new System.Drawing.Size(250, 23);
            this.CustomerListButton.TabIndex = 1;
            this.CustomerListButton.Text = "Customer List";
            this.CustomerListButton.UseVisualStyleBackColor = true;
            this.CustomerListButton.Click += new System.EventHandler(this.CustomerListButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(16, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Make sure the IDE Output window is open";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 141);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CustomerListButton);
            this.Controls.Add(this.GetCustomerByIdentifierButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In memory code sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetCustomerByIdentifierButton;
        private System.Windows.Forms.Button CustomerListButton;
        private System.Windows.Forms.Label label1;
    }
}

