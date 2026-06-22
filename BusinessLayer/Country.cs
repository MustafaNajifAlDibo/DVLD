using DataLayer;
using ModelLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer {
    public class Country {

        public static async Task<List<CountryDTO>> GetAllCountriesAsync() {
            return await CountryData.GetAllCountriesAsync();
        }
    }
}
