using System.Windows.Forms;

namespace PresentationLayer.UCs {
    public partial class AddUpdateUC : UserControl {

        public AddUpdateUC() {
            InitializeComponent();
        }

        public void SetAddNewPersonMode() {
            AddUpdatLabel.Text = "Add New Person";
        }
        public void SetUpdatePersonMode(int personID) {
            AddUpdatLabel.Text = "Update Person";
        }
    }
}
