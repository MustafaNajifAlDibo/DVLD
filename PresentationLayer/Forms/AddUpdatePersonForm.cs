using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.Forms {
    public partial class AddUpdatePersonForm : Form {

        public int PersonID { get; private set;  }

        public  AddUpdatePersonForm(int personID) {
            InitializeComponent();

            if(personID == -1) {
                _= SetAddNewPersonMode();
            } else {
                _= SetUpdatePersonMode(personID);
            }
        }

        private async Task SetAddNewPersonMode() {
            PersonID = -1;
            await addUpdateUC1.SetAddNewPersonMode();
            this.Text = "Add New Person";
        }
        private async Task SetUpdatePersonMode(int personID) {
            PersonID = personID;
            await addUpdateUC1.SetUpdatePersonMode(personID);
            this.Text = "Update Person";
        }
    }
}
