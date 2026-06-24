namespace PresentationLayer.Forms {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PeopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DriversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AccountSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApplicationsToolStripMenuItem,
            this.PeopleToolStripMenuItem,
            this.DriversToolStripMenuItem,
            this.UsersToolStripMenuItem,
            this.AccountSettingsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(965, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // ApplicationsToolStripMenuItem
            // 
            this.ApplicationsToolStripMenuItem.Name = "ApplicationsToolStripMenuItem";
            this.ApplicationsToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.ApplicationsToolStripMenuItem.Text = "Applications";
            // 
            // PeopleToolStripMenuItem
            // 
            this.PeopleToolStripMenuItem.Name = "PeopleToolStripMenuItem";
            this.PeopleToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.PeopleToolStripMenuItem.Text = "People";
            this.PeopleToolStripMenuItem.Click += new System.EventHandler(this.PeopleToolStripMenuItem_Click);
            // 
            // DriversToolStripMenuItem
            // 
            this.DriversToolStripMenuItem.Name = "DriversToolStripMenuItem";
            this.DriversToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.DriversToolStripMenuItem.Text = "Drivers";
            // 
            // UsersToolStripMenuItem
            // 
            this.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem";
            this.UsersToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.UsersToolStripMenuItem.Text = "Users";
            this.UsersToolStripMenuItem.Click += new System.EventHandler(this.UsersToolStripMenuItem_Click);
            // 
            // AccountSettingsToolStripMenuItem
            // 
            this.AccountSettingsToolStripMenuItem.Name = "AccountSettingsToolStripMenuItem";
            this.AccountSettingsToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.AccountSettingsToolStripMenuItem.Text = "Account Settings";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 490);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PeopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DriversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AccountSettingsToolStripMenuItem;
    }
}