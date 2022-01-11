using System.Collections.Generic;
using VidyaVahini.Entities.Gender;
using VidyaVahini.Infrastructure.Logger;
using VidyaVahini.Repository.Gender;

namespace VidyaVahini.Services.Gender
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
