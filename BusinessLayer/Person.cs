using DataLayer;
using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer {
    public class Person {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public PersonDTO PersonDTO { get; set; }

        public Person(){
                PersonDTO = new PersonDTO {
                PersonID = -1,
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
                NationalityCountryID = -1,
                ImagePath = string.Empty
            };
            Mode = enMode.AddNew;
        }

        public Person(PersonDTO personDTO) {
            PersonDTO = personDTO;

            Mode = enMode.Update;
        }

        public static Task<List<PersonDTO>> GetAllPeopleAsync() {
            return PersonData.GetAllPeopleAync();
        }

        public static async Task<PersonDTO> FindAsync(int personId) {
            return await PersonData.GetPersonByIDAsync(personId);
        }

        public async Task<bool> SaveAsync() {
            if (Mode == enMode.AddNew) {
                if (await _AddNewPerson()) {
                    Mode = enMode.Update;
                    return true;

                } else 
                    return false;
            } else {
                    return await _UpdatePerson();
            }
        }

        public static async Task<bool> DeleteAsync(int personId) {
            return await PersonData.DeletePersonAsync(personId);
        }

        public static async Task<bool> IsPersonExsitAsync(int personId) {
            return await PersonData.IsPersonExsitAsync(personId);
        }

        private async Task<bool> _AddNewPerson() {
            this.PersonDTO.PersonID = await PersonData.AddNewPersonAsync(PersonDTO);
            return this.PersonDTO.PersonID != -1;
        }

        private async Task<bool> _UpdatePerson() {
            return await PersonData.UpdatePersonAsync(PersonDTO);
        }
    }
}
