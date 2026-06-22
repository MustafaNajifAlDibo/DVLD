using ModelLayer.DTOs;
using System;
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
                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonID")),

                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),

                                SecondName = reader.GetString(reader.GetOrdinal("SecondName")),

                                ThirdName = reader.GetString(reader.GetOrdinal("ThirdName")),

                                LastName = reader.GetString(reader.GetOrdinal("LastName")),

                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),

                                Gender = reader.GetByte(reader.GetOrdinal("Gendor")),

                                Address = reader.IsDBNull( reader.GetOrdinal("Address"))
                                    ? string.Empty: reader.GetString(reader.GetOrdinal("Address")),

                                NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),

                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone"))
                                    ? string.Empty: reader.GetString(reader.GetOrdinal("Phone")),

                                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                                    ? string.Empty: reader.GetString(reader.GetOrdinal("Email")),

                                NationalityCountryId = reader.GetInt32(
                                    reader.GetOrdinal("NationalityCountryID")),

                                ImagePath = reader.IsDBNull( reader.GetOrdinal("ImagePath"))
                                    ? string.Empty: reader.GetString(reader.GetOrdinal("ImagePath"))
                            });
                        }
                    }
                }
            }
            return people;
        }

        public static async Task<PersonDTO> GetPersonByIDAsync(int personID) {
            PersonDTO personDTO = null;

            using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"
                SELECT PersonID,FirstName,SecondName,ThirdName,LastName,
                    DateOfBirth,Gendor,Address,NationalNo,Phone,Email,
                    NationalityCountryID,ImagePath FROM People Where PersonID = @PersonID";

                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();

                    using (SqlDataReader reader =
                       await command.ExecuteReaderAsync()) {
                        if (await reader.ReadAsync()) {
                            personDTO = new PersonDTO {
                                PersonId = reader.GetInt32(reader.GetOrdinal("PersonID")),

                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),

                                SecondName = reader.GetString(reader.GetOrdinal("SecondName")),

                                ThirdName = reader.GetString(reader.GetOrdinal("ThirdName")),

                                LastName = reader.GetString(reader.GetOrdinal("LastName")),

                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),

                                Gender = reader.GetByte(reader.GetOrdinal("Gendor")),

                                Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                                    ? string.Empty : reader.GetString(reader.GetOrdinal("Address")),

                                NationalNo = reader.GetString(reader.GetOrdinal("NationalNo")),

                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone"))
                                    ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),

                                Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                                    ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),

                                NationalityCountryId = reader.GetInt32(
                                    reader.GetOrdinal("NationalityCountryID")),

                                ImagePath = reader.IsDBNull(reader.GetOrdinal("ImagePath"))
                                    ? string.Empty : reader.GetString(reader.GetOrdinal("ImagePath"))
                            };

                            return personDTO;
                        } else {
                            return null;
                        }
                    }
                }
            }
        }

        public static async Task<int> AddNewPersonAsync(PersonDTO personDTO) {
            int newPersonID = -1;
            using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"
                INSERT INTO People (FirstName,SecondName,ThirdName,LastName,
                    DateOfBirth,Gendor,Address,NationalNo,Phone,Email,
                    NationalityCountryID,ImagePath)
                VALUES (@FirstName,@SecondName,@ThirdName,@LastName,
                    @DateOfBirth,@Gendor,@Address,@NationalNo,@Phone,@Email,
                    @NationalityCountryID,@ImagePath);
                SELECT SCOPE_IDENTITY();";
                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                    command.Parameters.AddWithValue("@SecondName", personDTO.SecondName);
                    command.Parameters.AddWithValue("@ThirdName", personDTO.ThirdName);
                    command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", personDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor",personDTO.Gender);
                    command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(personDTO.Address)
                        ? (object)DBNull.Value : personDTO.Address);
                    command.Parameters.AddWithValue("@NationalNo", personDTO.NationalNo);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(personDTO.Phone)
                        ? (object)DBNull.Value : personDTO.Phone);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(personDTO.Email)
                        ? (object)DBNull.Value : personDTO.Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", personDTO.NationalityCountryId);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(personDTO.ImagePath)
                        ? (object)DBNull.Value : personDTO.ImagePath);

                    connection.Open();
                    object result = await command.ExecuteScalarAsync();

                    if(result != null && int.TryParse(result.ToString(), out newPersonID)) {
                        return newPersonID;
                    } else {
                        throw new Exception("Failed to retrieve the new Person ID.");
                    }
                }
            }
        }

        public static async Task UpdatePersonAsync(PersonDTO personDTO) {
            using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"
                UPDATE People SET FirstName = @FirstName,SecondName = @SecondName,
                    ThirdName = @ThirdName,LastName = @LastName,DateOfBirth = @DateOfBirth,
                    Gendor = @Gendor,Address = @Address, NationalNo = @NationalNo,
                    Phone = @Phone, Email = @Email, NationalityCountryID = @NationalityCountryID,
                    ImagePath = @ImagePath WHERE PersonID = @PersonID";
                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@PersonID", personDTO.PersonId);
                    command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                    command.Parameters.AddWithValue("@SecondName", personDTO.SecondName);
                    command.Parameters.AddWithValue("@ThirdName", personDTO.ThirdName);
                    command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", personDTO.DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor",, personDTO.Gender);
                    command.Parameters.AddWithValue("@Address", string.IsNullOrEmpty(personDTO.Address)
                        ? (object)DBNull.Value : personDTO.Address);
                    command.Parameters.AddWithValue("@NationalNo", personDTO.NationalNo);
                    command.Parameters.AddWithValue("@Phone", string.IsNullOrEmpty(personDTO.Phone)
                        ? (object)DBNull.Value : personDTO.Phone);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(personDTO.Email)
                        ? (object)DBNull.Value : personDTO.Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", personDTO.NationalityCountryId);
                    command.Parameters.AddWithValue("@ImagePath", string.IsNullOrEmpty(personDTO.ImagePath)
                        ? (object)DBNull.Value : personDTO.ImagePath);
                }
            }
        }

        public static async Task DeletePersonAsync(int personID) {
            using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"DELETE FROM People WHERE PersonID = @PersonID";
                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<bool> IsPersonExsitAsync(int personID) {
            bool exists = false;

            using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"SELECT COUNT(1) FROM People WHERE PersonID = @PersonID";
                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@PersonID", personID);
                    connection.Open();
                    int count = (int)await command.ExecuteScalarAsync();
                    return exists = count > 0;
                }
            }
        }
    }
}
