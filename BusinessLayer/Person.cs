using DataLayer;
using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer {
    public class Person {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        PersonDTO _personDTO;

        public Person(){
            _personDTO = new PersonDTO {
                PersonId = -1,
                FirstName = string.Empty,
                SecondName = string.Empty,
                ThirdName = string.Empty,
                LastName = string.Empty,
                DateOfBirth = DateTime.Now,
                Gender = 0,
                Address = string.Empty,
                NationalNo = string.Empty,
                Phone = string.Empty,
                Email = string.Empty,
                NationalityCountryId = -1,
                ImagePath = string.Empty
            };
            Mode = enMode.AddNew;
        }

        public Person(PersonDTO personDTO) {
            _personDTO = personDTO;

            Mode = enMode.Update;
        }

        public static Task<List<PersonDTO>> GetAllPeopleAsync() {
            return PersonData.GetAllPeopleAync();
        }
    }
}
