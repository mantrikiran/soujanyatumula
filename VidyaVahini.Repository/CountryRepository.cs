using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Country;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
   public class CountryRepository : ICountryRepository
    {


        private readonly IUnitOfWork _unitOfWork;

        public CountryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CountryData> GetCountryData()
            => _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.Country>()
                .GetAll()
                .Select(x => new CountryData()
                {
                    countryid = x.countryid,
                    phone_code = x.phone_code,
                    country_name=x.country_name
                });

    }
}
