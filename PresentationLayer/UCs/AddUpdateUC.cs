using BusinessLayer;
using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentValidation.Results;
using DataLayer.InputValidation;

namespace PresentationLayer.UCs {
    public partial class AddUpdateUC : UserControl {

        private int defultCountryIndex = 89; // Jordan is the default country
        private int _personID = -1;
        public AddUpdateUC() {
            InitializeComponent();
        }

        public async Task SetAddNewPersonMode(int personID) {
            _personID = personID;
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
            _personID = personID;
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

        private async void SaveButton_Click(object sender, EventArgs e) {
            Person person = new Person {
                PersonDTO = new PersonDTO {
                    PersonId = _personID,
                    FirstName = FirstNameTextBox.Text,
                    SecondName = SecondNameTextBox.Text,
                    ThirdName = ThirdNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    DateOfBirth = DateOfBirthDateTimePicker.Value,
                    Gender = MaleRadioButton.Checked ? 0 : 1,
                    Address = AddressTextBox.Text,
                    NationalNo = NationalNoTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    Email = EmailTextBox.Text,
                    NationalityCountryId = CountryComboBox.SelectedIndex + 1,
                    ImagePath = string.Empty
                }
            };

            var validator = new PersonDTOValidator();

            ValidationResult result = validator.Validate(person.PersonDTO);

            if (!result.IsValid) {
                // عرض الأخطاء

                MessageBox.Show($"{result.Errors[0].ErrorMessage}");
                return;
            }

            if (_personID == -1) {
                person.Mode = Person.enMode.AddNew;
            } else {
                person.Mode = Person.enMode.Update;
            }

            await person.SaveAsync();

            ParentForm?.Close();
        }
    }
}
