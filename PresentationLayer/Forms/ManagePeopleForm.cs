using BusinessLayer;
using System;
using System.Drawing;
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
    }
}
