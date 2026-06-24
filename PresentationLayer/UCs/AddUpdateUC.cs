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
        private string _selectedImagePath = string.Empty;
        private int _personID = -1;
        public AddUpdateUC() {
            InitializeComponent();
        }

        public async Task SetAddNewPersonMode(int personID) {
            _personID = personID;
            AddUpdatLabel.Text = "Add New Person";
            PersonIDLabel.Text = "N/A";

            DateOfBirthDateTimePicker.MaxDate = DateTime.Today.AddYears(-18);
            CountryComboBox.DataSource = await Country.GetCountriesNameListAsync();
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

            CountryComboBox.DataSource = await Country.GetCountriesNameListAsync();
            CountryComboBox.SelectedIndex = personDto.NationalityCountryID - 1;
            _selectedImagePath = personDto.ImagePath;
            ProfileImagePictureBox.ImageLocation = _selectedImagePath;
        }


        private void CloseButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show(
                "Any unsaved changes will be lost. Do you want to close this window?",
                    "Confirm Close", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                == DialogResult.Yes) {

                ParentForm?.Close();
            }
        }

        private async void SaveButton_Click(object sender, EventArgs e) {

            Person person = new Person {
                PersonDTO = new PersonDTO {
                    PersonID = _personID,
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
                    NationalityCountryID = CountryComboBox.SelectedIndex + 1
                    
                }
            };

            var validator = new PersonDTOValidator();

            ValidationResult result = validator.Validate(person.PersonDTO);

            if (!result.IsValid) {
                // عرض الأخطاء

                MessageBox.Show($"{result.Errors[0].ErrorMessage}");
                return;
            }

            // Set person Image
            if (ProfileImagePictureBox.ImageLocation == string.Empty) {
                ProfileImageManager.DeleteImage(_selectedImagePath);
                _selectedImagePath = string.Empty;
            } else if (_selectedImagePath != ProfileImagePictureBox.ImageLocation) {
                _selectedImagePath = ProfileImageManager.ReplaceImage(
                    _selectedImagePath, ProfileImagePictureBox.ImageLocation);
            }

            person.PersonDTO.ImagePath = _selectedImagePath;

            if (_personID == -1) {
                person.Mode = Person.enMode.AddNew;
            } else {
                person.Mode = Person.enMode.Update;
            }

            if (await person.SaveAsync()) {
                MessageBox.Show("Person saved successfully.");

            } else {
                MessageBox.Show("Failed to save person.");
            }

            ParentForm?.Close();
        }

        private void SetImageLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            using (OpenFileDialog dialog = new OpenFileDialog()) {

                dialog.Title = "Select Profile Image";

                dialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                ProfileImagePictureBox.ImageLocation = dialog.FileName;
            }
        }

        private void RemoveImageLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            ProfileImagePictureBox.ImageLocation = string.Empty;
        }
    }
}
