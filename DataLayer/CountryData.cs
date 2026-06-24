
using ModelLayer.DTOs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataLayer {
    public class CountryData {
        public static async Task<List<CountryDTO>> GetAllCountriesAsync() {
            List<CountryDTO> countries = new List<CountryDTO>();

            using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"
                SELECT CountryID,CountryName FROM Countries";

                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    connection.Open();

                    using (SqlDataReader reader =
                       await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            countries.Add(new CountryDTO {
                                CountryID = reader.GetInt32(reader.GetOrdinal("CountryID")),

                                CountryName = reader.GetString(reader.GetOrdinal("CountryName"))
                            });
                        }
                    }
                }
            }
            return countries;
        }

        public static async Task<string> GetCountryNameByIDAsync(int CountryID) {
            string CountryName = null;
            using (SqlConnection connection =
            new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"
                SELECT CountryName FROM Countries
                    WHERE CountryID = @CountryID";

                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    command.Parameters.AddWithValue("@CountryID", CountryID);
                    connection.Open();


                    using (SqlDataReader reader =
                       await command.ExecuteReaderAsync()) {
                        if (await reader.ReadAsync()) {

                            return CountryName = reader.GetString(reader.GetOrdinal("CountryName"));
                            
                        }
                    }
                }
                return CountryName;
            }
        }
    }
}
