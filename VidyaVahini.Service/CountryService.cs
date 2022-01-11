
using System;
using System.Collections.Generic;
using System.Text;
using VidyaVahini.Entities.Country;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
  public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger _logger;
        
        public CountryService(ILogger logger, ICountryRepository countryRepository)
        {
            _logger = logger;
            _countryRepository = countryRepository;
        }

        public IEnumerable<CountryData> GetCountryData()
            => _countryRepository.GetCountryData();

        public CountryModel GetCountries()
            => new CountryModel
            {
                Countries = GetCountryData()
            };

    }

}
