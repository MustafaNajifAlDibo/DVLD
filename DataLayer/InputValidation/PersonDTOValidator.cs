using FluentValidation;
using ModelLayer.DTOs;
using System;
using System.Data.SqlClient;

namespace DataLayer.InputValidation {
    public class PersonDTOValidator : AbstractValidator<PersonDTO> {
        public PersonDTOValidator() {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.");

            RuleFor(x => x.SecondName)
                .NotEmpty()
                .WithMessage("SecondName is required.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.");

            RuleFor(x => x.NationalNo)
                .NotEmpty()
                .WithMessage("National Number is required.")
                .Must((person, nationalNo) =>
                    !ExistsNationalNo(nationalNo, person.PersonID))
                .WithMessage("National Number already exists.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.Email))
                .Must((person, email) =>
                    !ExistsEmail(email, person.PersonID))
                .WithMessage("Email already exists.");

            RuleFor(x => x.DateOfBirth)
                .LessThanOrEqualTo(DateTime.Today.AddYears(-18))
                .WithMessage("Person must be at least 18 years old.");

            RuleFor(x => x.NationalityCountryID)
                .GreaterThan(0);

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.");
        }

        public static bool ExistsNationalNo(string nationalNo, int personId) {
            bool exists = false;

            string query = @"
        SELECT 1
        FROM People
        WHERE NationalNo = @NationalNo
          AND PersonID <> @PersonID";

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@NationalNo", nationalNo);
                command.Parameters.AddWithValue("@PersonID", personId);

                connection.Open();

                exists = command.ExecuteScalar() != null;
            }

            return exists;
        }

        public static bool ExistsEmail(string email, int personId) {
            bool exists = false;

            string query = @"
        SELECT 1
        FROM People
        WHERE Email = @Email
          AND PersonID <> @PersonID";

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PersonID", personId);

                connection.Open();

                exists = command.ExecuteScalar() != null;
            }

            return exists;
        }
    }
}
