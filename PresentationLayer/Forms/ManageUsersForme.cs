using BusinessLayer;
using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Forms {
    public partial class ManageUsersForme : Form {

        private List<UserDTO> _allUsers;
        private string filterText;

        public ManageUsersForme() {
            InitializeComponent();

            FilterComboBox.SelectedIndex = 0;
            RetrieveAllUsersInDatatGridView();
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            Close();
        }

        private async void RetrieveAllUsersInDatatGridView() {

            _allUsers = await User.GetAllUsersAsync();
            PerformFilter(FilterTextBox.Text.Trim());
            UsersDataGridView.DataSource = _allUsers;

            RecordsLabel.Text = $"Records: {UsersDataGridView.Rows.Count}";
        }

        private void PerformFilter(string text) {

            switch (filterText) {

                case "User ID":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => p.PersonID.ToString().Contains(text)).ToList();
                    break;
                case "Person ID":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => p.PersonID.ToString().Contains(text)).ToList();
                    break;
                case "Full Name":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => p.FullName.Contains(text)).ToList();
                    break;
                case "User Name":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => p.UserName.Contains(text)).ToList();
                    break;
                case "Active":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => p.IsActive).ToList();
                    break;
                case "Not Active":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => !p.IsActive).ToList();
                    break;
                default:
                    break;
            }
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            FilterTextBox.Visible = true;
            filterText = FilterComboBox.SelectedItem.ToString();
            FilterTextBox.Text = string.Empty;

            if (filterText == "None"
                || filterText == "Active"
                || filterText == "Not Active") FilterTextBox.Visible = false;

            switch (filterText) {

                case "Active":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => p.IsActive).ToList();
                    break;
                case "Not Active":
                    UsersDataGridView.DataSource =
                _allUsers.Where(p => !p.IsActive).ToList();
                    break;
                default:
                    break;
            }

            FilterTextBox.Focus();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e) {
            string text = FilterTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(text)) {
                UsersDataGridView.DataSource = _allUsers;
                return;
            }

            PerformFilter(text);
        }

        private void FilterTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (FilterComboBox.Text == "Person ID"
                || FilterComboBox.Text == "User ID") {
                if (!char.IsControl(e.KeyChar) &&
                    !char.IsDigit(e.KeyChar)) {
                    e.Handled = true;
                }
            }
        }
    }
}
