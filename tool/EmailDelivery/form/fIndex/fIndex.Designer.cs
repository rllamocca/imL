﻿namespace EmailDelivery
{
    partial class fIndex
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            List1_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            List11_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            List1n_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { List1_ToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // List1_ToolStripMenuItem
            // 
            List1_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { List11_ToolStripMenuItem, List1n_ToolStripMenuItem });
            List1_ToolStripMenuItem.Name = "List1_ToolStripMenuItem";
            List1_ToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            List1_ToolStripMenuItem.Text = "Archivo";
            // 
            // List11_ToolStripMenuItem
            // 
            List11_ToolStripMenuItem.Name = "List11_ToolStripMenuItem";
            List11_ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            List11_ToolStripMenuItem.Text = "Configuración";
            List11_ToolStripMenuItem.Click += List11_ToolStripMenuItem_Click;
            // 
            // List1n_ToolStripMenuItem
            // 
            List1n_ToolStripMenuItem.Name = "List1n_ToolStripMenuItem";
            List1n_ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            List1n_ToolStripMenuItem.Text = "Salir";
            // 
            // fIndex
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "fIndex";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "...";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem List1_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem List11_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem List1n_ToolStripMenuItem;
    }
}
