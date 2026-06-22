using BusinessLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer.Forms {
    public partial class ManagePeopleForm : Form {
        public ManagePeopleForm() {
            InitializeComponent();

            RetrieveAllPeopleInDatatGridView();
        }

        private async void RetrieveAllPeopleInDatatGridView() {
            PeopleDataGridView.DataSource = await Person.GetAllPeopleAsync();

            PeopleDataGridView.Columns["Gender"].Visible = false;
            PeopleDataGridView.Columns["GenderText"].HeaderText = "Gender";

            RecordsLabel.Text = $"Records: {PeopleDataGridView.Rows.Count}";
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void AddPersonButton_Click(object sender, EventArgs e) {
            AddUpdatePersonForm addPersonForm = new AddUpdatePersonForm(-1);
            addPersonForm.ShowDialog();
        }
    }
}
