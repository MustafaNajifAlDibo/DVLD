using System;

namespace ModelLayer.DTOs {
    public class PersonDTO {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public string GenderText => Gender == 0 ? "Male" : "Female";
        public string Address { get; set; }
        public string NationalNo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
