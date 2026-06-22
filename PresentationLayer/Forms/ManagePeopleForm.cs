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
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
