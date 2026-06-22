using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Forms {
    public partial class AddUpdatePersonForm : Form {

        public int PersonID { get; private set;  }

        public  AddUpdatePersonForm(int personID) {
            InitializeComponent();

            if(personID == -1) {
                SetAddNewPersonMode();
            } else {
                _= SetUpdatePersonMode(personID);
            }
        }

        private void SetAddNewPersonMode() {
            PersonID = -1;
            addUpdateUC1.SetAddNewPersonMode();
            this.Text = "Add New Person";
        }
        private async Task SetUpdatePersonMode(int personID) {
            PersonID = personID;
            await addUpdateUC1.SetUpdatePersonMode(personID);
            this.Text = "Update Person";
        }
    }
}
