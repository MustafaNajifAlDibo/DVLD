using System;
using System.Windows.Forms;

namespace PresentationLayer.Forms {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        ManagePeopleForm managePeopleForm;
        private void PeopleToolStripMenuItem_Click(object sender, EventArgs e) {
            if(managePeopleForm == null || managePeopleForm.IsDisposed) {
                managePeopleForm = new ManagePeopleForm {
                    MdiParent = this
                };
                managePeopleForm.Show();
            } else {
                managePeopleForm.Focus();
            }
        }

        ManageUsersForme manageUsersForm;
        private void UsersToolStripMenuItem_Click(object sender, EventArgs e) {
            if (manageUsersForm == null || manageUsersForm.IsDisposed) {
                manageUsersForm = new ManageUsersForme {
                    MdiParent = this
                };
                manageUsersForm.Show();
            } else {
                manageUsersForm.Focus();
            }
        }
    }
}
