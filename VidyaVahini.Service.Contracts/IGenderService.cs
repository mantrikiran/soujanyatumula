using System.Collections.Generic;
using VidyaVahini.Entities.Gender;

namespace VidyaVahini.Service.Contracts
{
    public interface IGenderService
    {
        /// <summary>
        /// Gets all the genders
        /// </summary>
        GenderModel GetGenders();

        /// <summary>
        /// Gets all the genders
        /// </summary>
        IEnumerable<GenderData> GetGenderData();
    }
}
