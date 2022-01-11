using System;
using System.Collections.Generic;
using System.Text;
using VidyaVahini.Entities.Country;

namespace VidyaVahini.Service.Contracts
{
    public interface ICountryService
    {
        /// <summary>
        /// Gets the countries
        /// </summary>
        CountryModel GetCountries();

        /// <summary>
        /// Gets the countries
        /// </summary>
        IEnumerable<CountryData> GetCountryData();
    }

}
