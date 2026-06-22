using BusinessLayer;
using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer.UCs {
    public partial class AddUpdateUC : UserControl {

        private int defultCountryIndex = 89; // Jordan is the default country

        public AddUpdateUC() {
            InitializeComponent();
        }

        public async Task SetAddNewPersonMode() {
            AddUpdatLabel.Text = "Add New Person";
            PersonIDLabel.Text = "N/A";

            DateOfBirthDateTimePicker.MaxDate = DateTime.Today.AddYears(-18);
            CountryComboBox.DataSource = await GetCountriesNameListAsync();
            CountryComboBox.SelectedIndex = defultCountryIndex;
            FirstNameTextBox.Text = string.Empty;
            SecondNameTextBox.Text = string.Empty;
            ThirdNameTextBox.Text = string.Empty;
            LastNameTextBox.Text = string.Empty;
            NationalNoTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            AddressTextBox.Text = string.Empty;
            MaleRadioButton.Checked = true;
            FemaleRadioButton.Checked = false;
        }
        public async Task SetUpdatePersonMode(int personID) {
            AddUpdatLabel.Text = "Update Person";
            PersonIDLabel.Text = personID.ToString();

            PersonDTO personDto = await Person.FindAsync(personID);

            FirstNameTextBox.Text = personDto.FirstName;
            SecondNameTextBox.Text = personDto.LastName;
            ThirdNameTextBox.Text = personDto.ThirdName;
            LastNameTextBox.Text = personDto.LastName;
            NationalNoTextBox.Text = personDto.NationalNo;
            DateOfBirthDateTimePicker.Value = personDto.DateOfBirth;
            PhoneTextBox.Text = personDto.Phone;
            EmailTextBox.Text = personDto.Email;
            AddressTextBox.Text = personDto.Address;
            if (personDto.Gender == 0) {
                MaleRadioButton.Checked = true;
            } else {
                FemaleRadioButton.Checked = true;
            }

            CountryComboBox.DataSource = await GetCountriesNameListAsync();
            CountryComboBox.SelectedIndex = personDto.NationalityCountryId -1;
        }

        private async Task<List<string>> GetCountriesNameListAsync() {
            List<CountryDTO> countries = await Country.GetAllCountriesAsync();
            List<string> countryNames = new List<string>();
            foreach (var country in countries) {
                countryNames.Add(country.CountryName);
            }
            return countryNames;
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show(
                "Any unsaved changes will be lost. Do you want to close this window?",
                    "Confirm Close",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) 
                == DialogResult.Yes) {

                ParentForm?.Close();
            }
        }
    }
}
