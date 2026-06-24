using DataLayer;
using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer {
    public class User {



        public static async Task<List<UserDTO>> GetAllUsersAsync() {
            return await UserData.GetAllUsersAync();
        }
    }
}
