using DataLayer;
using ModelLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer {
    public class Country {

        public static async Task<List<CountryDTO>> GetAllCountriesAsync() {
            return await CountryData.GetAllCountriesAsync();
        }

        public static async Task<List<string>> GetCountriesNameListAsync() {
            List<CountryDTO> countries = await GetAllCountriesAsync();
            List<string> countryNames = new List<string>();
            foreach (var country in countries) {
                countryNames.Add(country.CountryName);
            }
            return countryNames;
        }
    }
}
