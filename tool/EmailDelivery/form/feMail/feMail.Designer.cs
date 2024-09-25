namespace EmailDelivery
{
    partial class feMail
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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            _Code = new System.Windows.Forms.TextBox();
            _Send = new System.Windows.Forms.CheckBox();
            _Body = new System.Windows.Forms.RichTextBox();
            _Result = new System.Windows.Forms.RichTextBox();
            _Subject = new System.Windows.Forms.RichTextBox();
            _To = new System.Windows.Forms.ListBox();
            _CC = new System.Windows.Forms.ListBox();
            _BCC = new System.Windows.Forms.ListBox();
            _PathAttachments = new System.Windows.Forms.ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(16, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(49, 15);
            label1.TabIndex = 0;
            label1.Text = "Código:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(16, 45);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(42, 15);
            label2.TabIndex = 1;
            label2.Text = "Enviar:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(16, 65);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(33, 15);
            label3.TabIndex = 2;
            label3.Text = "Para:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(16, 135);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(26, 15);
            label4.TabIndex = 3;
            label4.Text = "CC:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(16, 205);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(35, 15);
            label5.TabIndex = 4;
            label5.Text = "CCO:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(16, 275);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(48, 15);
            label6.TabIndex = 5;
            label6.Text = "Asunto:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(16, 321);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(49, 15);
            label7.TabIndex = 6;
            label7.Text = "Cuerpo:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(16, 387);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(53, 15);
            label8.TabIndex = 7;
            label8.Text = "Adjunto:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            label9.Location = new System.Drawing.Point(16, 457);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(65, 15);
            label9.TabIndex = 8;
            label9.Text = "Resultado:";
            // 
            // _Code
            // 
            _Code.Location = new System.Drawing.Point(150, 16);
            _Code.Name = "_Code";
            _Code.ReadOnly = true;
            _Code.Size = new System.Drawing.Size(320, 23);
            _Code.TabIndex = 9;
            // 
            // _Send
            // 
            _Send.AutoSize = true;
            _Send.Location = new System.Drawing.Point(150, 45);
            _Send.Name = "_Send";
            _Send.Size = new System.Drawing.Size(15, 14);
            _Send.TabIndex = 10;
            _Send.UseVisualStyleBackColor = true;
            // 
            // _Body
            // 
            _Body.Location = new System.Drawing.Point(150, 321);
            _Body.Name = "_Body";
            _Body.ReadOnly = true;
            _Body.Size = new System.Drawing.Size(320, 60);
            _Body.TabIndex = 15;
            _Body.Text = "";
            // 
            // _Result
            // 
            _Result.Location = new System.Drawing.Point(150, 457);
            _Result.Name = "_Result";
            _Result.ReadOnly = true;
            _Result.Size = new System.Drawing.Size(320, 60);
            _Result.TabIndex = 17;
            _Result.Text = "";
            // 
            // _Subject
            // 
            _Subject.Location = new System.Drawing.Point(150, 275);
            _Subject.Name = "_Subject";
            _Subject.ReadOnly = true;
            _Subject.Size = new System.Drawing.Size(320, 40);
            _Subject.TabIndex = 18;
            _Subject.Text = "";
            // 
            // _To
            // 
            _To.FormattingEnabled = true;
            _To.ItemHeight = 15;
            _To.Location = new System.Drawing.Point(150, 65);
            _To.Name = "_To";
            _To.Size = new System.Drawing.Size(320, 64);
            _To.TabIndex = 19;
            // 
            // _CC
            // 
            _CC.FormattingEnabled = true;
            _CC.ItemHeight = 15;
            _CC.Location = new System.Drawing.Point(150, 135);
            _CC.Name = "_CC";
            _CC.Size = new System.Drawing.Size(320, 64);
            _CC.TabIndex = 20;
            // 
            // _BCC
            // 
            _BCC.FormattingEnabled = true;
            _BCC.ItemHeight = 15;
            _BCC.Location = new System.Drawing.Point(150, 205);
            _BCC.Name = "_BCC";
            _BCC.Size = new System.Drawing.Size(320, 64);
            _BCC.TabIndex = 21;
            // 
            // _PathAttachments
            // 
            _PathAttachments.FormattingEnabled = true;
            _PathAttachments.ItemHeight = 15;
            _PathAttachments.Location = new System.Drawing.Point(150, 387);
            _PathAttachments.Name = "_PathAttachments";
            _PathAttachments.Size = new System.Drawing.Size(320, 64);
            _PathAttachments.TabIndex = 22;
            // 
            // feMail
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(489, 541);
            Controls.Add(_PathAttachments);
            Controls.Add(_BCC);
            Controls.Add(_CC);
            Controls.Add(_To);
            Controls.Add(_Subject);
            Controls.Add(_Result);
            Controls.Add(_Body);
            Controls.Add(_Send);
            Controls.Add(_Code);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "feMail";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "eMail:";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox _Code;
        private System.Windows.Forms.CheckBox _Send;
        private System.Windows.Forms.RichTextBox _Body;
        private System.Windows.Forms.RichTextBox _Result;
        private System.Windows.Forms.RichTextBox _Subject;
        private System.Windows.Forms.ListBox _To;
        private System.Windows.Forms.ListBox _CC;
        private System.Windows.Forms.ListBox _BCC;
        private System.Windows.Forms.ListBox _PathAttachments;
    }
}