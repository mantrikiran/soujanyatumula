using System;
using System.Collections.Generic;
using System.Text;
using VidyaVahini.Entities.Country;

namespace VidyaVahini.Repository.Contracts
{
    public interface ICountryRepository
    {

        /// <summary>
        /// Gets all the countries from the database
        /// </summary>
        IEnumerable<CountryData> GetCountryData();
    }
}
