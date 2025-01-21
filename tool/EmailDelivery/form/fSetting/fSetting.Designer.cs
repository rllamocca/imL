namespace EmailDelivery
{
    partial class fSetting
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
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            _FromDisplayName = new System.Windows.Forms.TextBox();
            _FromAddress = new System.Windows.Forms.TextBox();
            label14 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            _SplitSeparator = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            _1_Password = new System.Windows.Forms.TextBox();
            _1_UserName = new System.Windows.Forms.TextBox();
            _1_UseDefaultCredentials = new System.Windows.Forms.CheckBox();
            _1_DeliveryFormat = new System.Windows.Forms.ComboBox();
            _1_DeliveryMethod = new System.Windows.Forms.ComboBox();
            _1_EnableSsl = new System.Windows.Forms.CheckBox();
            _1_Host = new System.Windows.Forms.TextBox();
            _1_PickupDirectoryLocation = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            _1_Port = new System.Windows.Forms.NumericUpDown();
            _1_TargetName = new System.Windows.Forms.TextBox();
            _1_Timeout = new System.Windows.Forms.NumericUpDown();
            label11 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            tabPage3 = new System.Windows.Forms.TabPage();
            button2 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            richTextBox1 = new System.Windows.Forms.RichTextBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_1_Port).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_1_Timeout).BeginInit();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new System.Drawing.Point(16, 16);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(530, 370);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(_FromDisplayName);
            tabPage1.Controls.Add(_FromAddress);
            tabPage1.Controls.Add(label14);
            tabPage1.Controls.Add(label13);
            tabPage1.Controls.Add(_SplitSeparator);
            tabPage1.Controls.Add(label12);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(522, 342);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // _FromDisplayName
            // 
            _FromDisplayName.Location = new System.Drawing.Point(200, 74);
            _FromDisplayName.Name = "_FromDisplayName";
            _FromDisplayName.Size = new System.Drawing.Size(300, 23);
            _FromDisplayName.TabIndex = 5;
            // 
            // _FromAddress
            // 
            _FromAddress.Location = new System.Drawing.Point(200, 45);
            _FromAddress.Name = "_FromAddress";
            _FromAddress.Size = new System.Drawing.Size(300, 23);
            _FromAddress.TabIndex = 4;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(16, 74);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(108, 15);
            label14.TabIndex = 3;
            label14.Text = "FromDisplayName:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(16, 45);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(80, 15);
            label13.TabIndex = 2;
            label13.Text = "FromAddress:";
            // 
            // _SplitSeparator
            // 
            _SplitSeparator.Location = new System.Drawing.Point(200, 16);
            _SplitSeparator.Name = "_SplitSeparator";
            _SplitSeparator.Size = new System.Drawing.Size(50, 23);
            _SplitSeparator.TabIndex = 1;
            _SplitSeparator.Text = ",";
            _SplitSeparator.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(16, 16);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(83, 15);
            label12.TabIndex = 0;
            label12.Text = "SplitSeparator:";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(_1_Password);
            tabPage2.Controls.Add(_1_UserName);
            tabPage2.Controls.Add(_1_UseDefaultCredentials);
            tabPage2.Controls.Add(_1_DeliveryFormat);
            tabPage2.Controls.Add(_1_DeliveryMethod);
            tabPage2.Controls.Add(_1_EnableSsl);
            tabPage2.Controls.Add(_1_Host);
            tabPage2.Controls.Add(_1_PickupDirectoryLocation);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(_1_Port);
            tabPage2.Controls.Add(_1_TargetName);
            tabPage2.Controls.Add(_1_Timeout);
            tabPage2.Controls.Add(label11);
            tabPage2.Controls.Add(label10);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(522, 342);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "...";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // _1_Password
            // 
            _1_Password.Location = new System.Drawing.Point(200, 298);
            _1_Password.Name = "_1_Password";
            _1_Password.Size = new System.Drawing.Size(300, 23);
            _1_Password.TabIndex = 43;
            _1_Password.UseSystemPasswordChar = true;
            // 
            // _1_UserName
            // 
            _1_UserName.Location = new System.Drawing.Point(200, 269);
            _1_UserName.Name = "_1_UserName";
            _1_UserName.Size = new System.Drawing.Size(300, 23);
            _1_UserName.TabIndex = 42;
            // 
            // _1_UseDefaultCredentials
            // 
            _1_UseDefaultCredentials.AutoSize = true;
            _1_UseDefaultCredentials.Location = new System.Drawing.Point(200, 244);
            _1_UseDefaultCredentials.Name = "_1_UseDefaultCredentials";
            _1_UseDefaultCredentials.Size = new System.Drawing.Size(15, 14);
            _1_UseDefaultCredentials.TabIndex = 41;
            _1_UseDefaultCredentials.UseVisualStyleBackColor = true;
            // 
            // _1_DeliveryFormat
            // 
            _1_DeliveryFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _1_DeliveryFormat.FormattingEnabled = true;
            _1_DeliveryFormat.Location = new System.Drawing.Point(200, 215);
            _1_DeliveryFormat.Name = "_1_DeliveryFormat";
            _1_DeliveryFormat.Size = new System.Drawing.Size(300, 23);
            _1_DeliveryFormat.TabIndex = 40;
            // 
            // _1_DeliveryMethod
            // 
            _1_DeliveryMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _1_DeliveryMethod.FormattingEnabled = true;
            _1_DeliveryMethod.Location = new System.Drawing.Point(200, 186);
            _1_DeliveryMethod.Name = "_1_DeliveryMethod";
            _1_DeliveryMethod.Size = new System.Drawing.Size(300, 23);
            _1_DeliveryMethod.TabIndex = 39;
            // 
            // _1_EnableSsl
            // 
            _1_EnableSsl.AutoSize = true;
            _1_EnableSsl.Location = new System.Drawing.Point(200, 161);
            _1_EnableSsl.Name = "_1_EnableSsl";
            _1_EnableSsl.Size = new System.Drawing.Size(15, 14);
            _1_EnableSsl.TabIndex = 38;
            _1_EnableSsl.UseVisualStyleBackColor = true;
            // 
            // _1_Host
            // 
            _1_Host.Location = new System.Drawing.Point(200, 132);
            _1_Host.Name = "_1_Host";
            _1_Host.Size = new System.Drawing.Size(300, 23);
            _1_Host.TabIndex = 37;
            // 
            // _1_PickupDirectoryLocation
            // 
            _1_PickupDirectoryLocation.Location = new System.Drawing.Point(200, 103);
            _1_PickupDirectoryLocation.Name = "_1_PickupDirectoryLocation";
            _1_PickupDirectoryLocation.Size = new System.Drawing.Size(300, 23);
            _1_PickupDirectoryLocation.TabIndex = 36;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(16, 103);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(140, 15);
            label4.TabIndex = 35;
            label4.Text = "PickupDirectoryLocation:";
            // 
            // _1_Port
            // 
            _1_Port.Location = new System.Drawing.Point(200, 74);
            _1_Port.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            _1_Port.Name = "_1_Port";
            _1_Port.Size = new System.Drawing.Size(75, 23);
            _1_Port.TabIndex = 34;
            _1_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            _1_Port.Value = new decimal(new int[] { 25, 0, 0, 0 });
            // 
            // _1_TargetName
            // 
            _1_TargetName.Location = new System.Drawing.Point(200, 45);
            _1_TargetName.Name = "_1_TargetName";
            _1_TargetName.Size = new System.Drawing.Size(300, 23);
            _1_TargetName.TabIndex = 33;
            // 
            // _1_Timeout
            // 
            _1_Timeout.Location = new System.Drawing.Point(200, 16);
            _1_Timeout.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            _1_Timeout.Name = "_1_Timeout";
            _1_Timeout.Size = new System.Drawing.Size(75, 23);
            _1_Timeout.TabIndex = 32;
            _1_Timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            _1_Timeout.ThousandsSeparator = true;
            _1_Timeout.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(16, 298);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(60, 15);
            label11.TabIndex = 31;
            label11.Text = "Password:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(16, 269);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(65, 15);
            label10.TabIndex = 30;
            label10.Text = "UserName:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(16, 244);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(126, 15);
            label9.TabIndex = 29;
            label9.Text = "UseDefaultCredentials:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(16, 215);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(90, 15);
            label8.TabIndex = 28;
            label8.Text = "DeliveryFormat:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(16, 186);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(94, 15);
            label7.TabIndex = 27;
            label7.Text = "DeliveryMethod:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(16, 161);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(59, 15);
            label6.TabIndex = 26;
            label6.Text = "EnableSsl:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(16, 132);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(35, 15);
            label5.TabIndex = 25;
            label5.Text = "Host:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 74);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(32, 15);
            label3.TabIndex = 24;
            label3.Text = "Port:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 45);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(74, 15);
            label2.TabIndex = 23;
            label2.Text = "TargetName:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(56, 15);
            label1.TabIndex = 22;
            label1.Text = "TimeOut:";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(richTextBox1);
            tabPage3.Controls.Add(button2);
            tabPage3.Location = new System.Drawing.Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(3);
            tabPage3.Size = new System.Drawing.Size(522, 342);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "OAuth 2.0";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(6, 6);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 0;
            button2.Text = "google";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(471, 392);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new System.Drawing.Point(6, 35);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new System.Drawing.Size(510, 301);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // fSetting
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(564, 431);
            Controls.Add(button1);
            Controls.Add(tabControl1);
            Name = "fSetting";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Configuración:";
            Load += fSetting_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_1_Port).EndInit();
            ((System.ComponentModel.ISupportInitialize)_1_Timeout).EndInit();
            tabPage3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox _1_Password;
        private System.Windows.Forms.TextBox _1_UserName;
        private System.Windows.Forms.CheckBox _1_UseDefaultCredentials;
        private System.Windows.Forms.ComboBox _1_DeliveryFormat;
        private System.Windows.Forms.ComboBox _1_DeliveryMethod;
        private System.Windows.Forms.CheckBox _1_EnableSsl;
        private System.Windows.Forms.TextBox _1_Host;
        private System.Windows.Forms.TextBox _1_PickupDirectoryLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _1_Port;
        private System.Windows.Forms.TextBox _1_TargetName;
        private System.Windows.Forms.NumericUpDown _1_Timeout;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _SplitSeparator;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox _FromDisplayName;
        private System.Windows.Forms.TextBox _FromAddress;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}