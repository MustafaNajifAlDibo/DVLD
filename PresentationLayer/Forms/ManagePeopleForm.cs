using BusinessLayer;
using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PresentationLayer.Forms {
    public partial class ManagePeopleForm : Form {

        private string filterText;
        private List<PersonDTO> _allPeople;

        public ManagePeopleForm() {
            InitializeComponent();
            FilterComboBox.SelectedIndex = 0;
            RetrieveAllPeopleInDatatGridView();
        }

        private async void RetrieveAllPeopleInDatatGridView() {

            _allPeople = await Person.GetAllPeopleAsync();
            PeopleDataGridView.DataSource = _allPeople;

            PeopleDataGridView.Columns["Gender"].Visible = false;
            PeopleDataGridView.Columns["GenderText"].HeaderText = "Gender";

            PeopleDataGridView.Columns["NationalityCountryID"].Visible = false;
            PeopleDataGridView.Columns["NationalityName"].HeaderText = "Nationality";

            RecordsLabel.Text = $"Records: {PeopleDataGridView.Rows.Count}";
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void AddPersonButton_Click(object sender, EventArgs e) {
            AddUpdatePersonForm addPersonForm = new AddUpdatePersonForm(-1);
            addPersonForm.ShowDialog();
            RetrieveAllPeopleInDatatGridView();
        }

        private void addNewPersonToolStripMenuItem1_Click(object sender, EventArgs e) {
            AddPersonButton_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e) {
            int personID = (int)PeopleDataGridView.CurrentRow.Cells["PersonId"].Value;

            AddUpdatePersonForm addPersonForm = new AddUpdatePersonForm(personID);
            addPersonForm.ShowDialog();
            RetrieveAllPeopleInDatatGridView();
        }

        // This is a simple way to select a row when hovering over it with the mouse.
        private int _hoverRow = -1;

        private void PeopleDataGridView_MouseMove(object sender, MouseEventArgs e) {
            var hit = PeopleDataGridView.HitTest(e.X, e.Y);

            if (hit.RowIndex == _hoverRow || hit.RowIndex < 0)
                return;

            _hoverRow = hit.RowIndex;

            PeopleDataGridView.ClearSelection();
            PeopleDataGridView.Rows[_hoverRow].Selected = true;
            PeopleDataGridView.CurrentCell =
                PeopleDataGridView.Rows[_hoverRow].Cells[0];
        }

        private async void DeleteToolStripMenuItem_Click(object sender, EventArgs e) {
            int personID = (int)PeopleDataGridView.CurrentRow.Cells["PersonId"].Value;
            try {
                if (await Person.DeleteAsync(personID)
                    && ProfileImageManager.DeleteImage((string)PeopleDataGridView.CurrentRow.Cells["ImagePath"].Value)) {
                    MessageBox.Show("Person deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RetrieveAllPeopleInDatatGridView();

                } else {
                    MessageBox.Show("Failed to delete person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (SqlException) {
                MessageBox.Show(
                    "This person cannot be deleted because related records exist.",
                    "Delete Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void FilterComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            FilterTextBox.Visible = true;
            filterText = FilterComboBox.SelectedItem.ToString();
            FilterTextBox.Text = string.Empty;

            if(filterText == "None") FilterTextBox.Visible = false;

            FilterTextBox.Focus();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e) {

            string text = FilterTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(text)) {
                PeopleDataGridView.DataSource = _allPeople;
                return;
            }

            switch (filterText) {

                case "First Name":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.FirstName.Contains(text)).ToList();
                    break;
                case "Second Name":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.SecondName.Contains(text)).ToList();
                    break;
                case "Third Name":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.ThirdName.Contains(text)).ToList();
                    break;
                case "Last Name":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.LastName.Contains(text)).ToList();
                    break;
                case "PersonID":
                    PeopleDataGridView.DataSource =
                _allPeople .Where(p => p.PersonID.ToString().Contains(text)).ToList();
                    break;
                case "Email":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.Email.Contains(text)).ToList();
                    break;
                case "Nationality":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.NationalityName.Contains(text)).ToList();
                    break;
                case "NationalNo":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.NationalNo.Contains(text)).ToList();
                    break;
                case "Phone":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.Phone.Contains(text)).ToList();
                    break;
                case "Gender":
                    PeopleDataGridView.DataSource =
                _allPeople.Where(p => p.GenderText.Contains(text)).ToList();
                    break;
                default:
                    break;
            }
        }

        private void FilterTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (FilterComboBox.Text == "PersonID") {
                if (!char.IsControl(e.KeyChar) &&
                    !char.IsDigit(e.KeyChar)) {
                    e.Handled = true;
                }
            }
        }
    }
}
