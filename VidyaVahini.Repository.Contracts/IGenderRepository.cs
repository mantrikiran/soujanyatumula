using System.Collections.Generic;
using VidyaVahini.Entities.Gender;

namespace VidyaVahini.Repository.Contracts
{
    public interface IGenderRepository
    {
        /// <summary>
        /// Gets all the genders from the database
        /// </summary>
        IEnumerable<GenderData> GetGenderData();
    }
}
