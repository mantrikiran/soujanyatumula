using System.Collections.Generic;
using VidyaVahini.Entities.Gender;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly ILogger _logger;

        public GenderService(ILogger logger, IGenderRepository genderRepository)
        {
            _logger = logger;
            _genderRepository = genderRepository;
        }

        public IEnumerable<GenderData> GetGenderData()
            => _genderRepository.GetGenderData();

        public GenderModel GetGenders()
            => new GenderModel
            {
                Genders = GetGenderData()
            };
    }
}
