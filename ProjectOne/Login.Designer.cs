namespace LoginNameSpace
{
    partial class Login
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            ipAddressText = new TextBox();
            usernameText = new TextBox();
            portText = new TextBox();
            passwordText = new TextBox();
            connectButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // ipAddressText
            // 
            ipAddressText.Location = new Point(79, 17);
            ipAddressText.Name = "ipAddressText";
            ipAddressText.Size = new Size(100, 23);
            ipAddressText.TabIndex = 7;
            // 
            // usernameText
            // 
            usernameText.Location = new Point(79, 46);
            usernameText.Name = "usernameText";
            usernameText.Size = new Size(100, 23);
            usernameText.TabIndex = 1;
            // 
            // portText
            // 
            portText.Location = new Point(234, 17);
            portText.Name = "portText";
            portText.Size = new Size(100, 23);
            portText.TabIndex = 6;
            // 
            // passwordText
            // 
            passwordText.Location = new Point(79, 75);
            passwordText.Name = "passwordText";
            passwordText.Size = new Size(100, 23);
            passwordText.TabIndex = 3;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(79, 126);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(75, 23);
            connectButton.TabIndex = 4;
            connectButton.Text = "OK";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(234, 126);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.DarkOrange;
            label1.Location = new Point(12, 20);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 8;
            label1.Text = "IP Address";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.DarkOrange;
            label2.Location = new Point(14, 49);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 9;
            label2.Text = "Username";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.DarkOrange;
            label3.Location = new Point(17, 78);
            label3.Name = "label3";
            label3.Size = new Size(57, 15);
            label3.TabIndex = 10;
            label3.Text = "Password";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.DarkOrange;
            label4.Location = new Point(190, 20);
            label4.Name = "label4";
            label4.Size = new Size(29, 15);
            label4.TabIndex = 11;
            label4.Text = "Port";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(370, 177);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(connectButton);
            Controls.Add(passwordText);
            Controls.Add(portText);
            Controls.Add(usernameText);
            Controls.Add(ipAddressText);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ipAddressText;
        private TextBox usernameText;
        private TextBox portText;
        private TextBox passwordText;
        private Button connectButton;
        private Button cancelButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}