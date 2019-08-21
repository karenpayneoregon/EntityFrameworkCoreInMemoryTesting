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
            this.SuspendLayout();
            // 
            // GetCustomerByIdentifierButton
            // 
            this.GetCustomerByIdentifierButton.Location = new System.Drawing.Point(12, 24);
            this.GetCustomerByIdentifierButton.Name = "GetCustomerByIdentifierButton";
            this.GetCustomerByIdentifierButton.Size = new System.Drawing.Size(167, 23);
            this.GetCustomerByIdentifierButton.TabIndex = 0;
            this.GetCustomerByIdentifierButton.Text = "button1";
            this.GetCustomerByIdentifierButton.UseVisualStyleBackColor = true;
            this.GetCustomerByIdentifierButton.Click += new System.EventHandler(this.GetCustomerByIdentifierButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GetCustomerByIdentifierButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In memory code sample";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetCustomerByIdentifierButton;
    }
}

