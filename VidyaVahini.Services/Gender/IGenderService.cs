using System.Collections.Generic;
using VidyaVahini.Entities.Gender;

namespace VidyaVahini.Services.Gender
{
    public interface IGenderService
    {
        GenderModel GetGenders();

        IEnumerable<GenderData> GetGenderData();
    }
}
