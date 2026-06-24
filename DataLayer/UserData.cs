

using ModelLayer.DTOs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataLayer {
    public class UserData {
        public static async Task<List<UserDTO>> GetAllUsersAync() {
            List<UserDTO> users = new List<UserDTO>();

            using (SqlConnection connection =
                new SqlConnection(DataAccessSettings.ConnectionString)) {
                const string query = @"
                SELECT        Users.UserID, Users.PersonID,CONCAT_WS(' ',
        People.FirstName,
        People.SecondName,
        People.ThirdName,
        People.LastName
    ) AS FullName, Users.UserName, Users.Password, Users.IsActive
FROM            Users INNER JOIN
                         People ON Users.PersonID = People.PersonID";

                using (SqlCommand command =
                    new SqlCommand(query, connection)) {
                    connection.Open();

                    using (SqlDataReader reader =
                       await command.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            users.Add(new UserDTO {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),

                                PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),

                                FullName = reader.GetString(reader.GetOrdinal("FullName")),

                                UserName = reader.GetString(reader.GetOrdinal("UserName")),

                                Password = reader.GetString(reader.GetOrdinal("Password")),

                                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                            });
                        }
                    }
                }
            }
            return users;
        }
    }
}
