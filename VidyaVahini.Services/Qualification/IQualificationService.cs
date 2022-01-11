using System.Collections.Generic;
using VidyaVahini.Entities.Qualification;

namespace VidyaVahini.Services.Qualification
{
    public interface IQualificationService
    {
        QualificationModel GetQualifications();

        IEnumerable<QualificationData> GetQualificationData();
    }
}