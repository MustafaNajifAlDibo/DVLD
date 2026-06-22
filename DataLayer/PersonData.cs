using ModelLayer.DTOs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataLayer {
    public static class PersonData {
        public static async Task<List<PersonDTO>> GetAllPeopleAync() {
            List<PersonDTO> people = new List<PersonDTO>();

        using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"
                SELECT PersonID,FirstName,SecondName,ThirdName,LastName,
                    DateOfBirth,Gendor,Address,NationalNo,Phone,Email,
                    NationalityCountryID,ImagePath FROM People";

                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    connection.Open();

                    using (SqlDataReader reader =
                       await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            people.Add(new PersonDTO {
                                PersonId = reader.GetInt32(
                                    reader.GetOrdinal("PersonID")),

                                FirstName = reader.GetString(
                                    reader.GetOrdinal("FirstName")),

                                SecondName = reader.GetString(
                                    reader.GetOrdinal("SecondName")),

                                ThirdName = reader.GetString(
                                    reader.GetOrdinal("ThirdName")),

                                LastName = reader.GetString(
                                    reader.GetOrdinal("LastName")),

                                DateOfBirth = reader.GetDateTime(
                                    reader.GetOrdinal("DateOfBirth")),

                                Gender = reader.GetByte(
                                    reader.GetOrdinal("Gendor")),

                                Address = reader.IsDBNull(
                                    reader.GetOrdinal("Address"))
                                    ? string.Empty
                                    : reader.GetString(
                                        reader.GetOrdinal("Address")),

                                NationalNo = reader.GetString(
                                    reader.GetOrdinal("NationalNo")),

                                Phone = reader.IsDBNull(
                                    reader.GetOrdinal("Phone"))
                                    ? string.Empty
                                    : reader.GetString(
                                        reader.GetOrdinal("Phone")),

                                Email = reader.IsDBNull(
                                    reader.GetOrdinal("Email"))
                                    ? string.Empty
                                    : reader.GetString(
                                        reader.GetOrdinal("Email")),

                                NationalityCountryId = reader.GetInt32(
                                    reader.GetOrdinal("NationalityCountryID")),

                                ImagePath = reader.IsDBNull(
                                    reader.GetOrdinal("ImagePath"))
                                    ? string.Empty
                                    : reader.GetString(
                                        reader.GetOrdinal("ImagePath"))
                            });
                        }
                    }
                }
            }

            return people;
        }
    }

}
