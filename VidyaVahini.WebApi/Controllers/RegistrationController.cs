using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Registration;
using VidyaVahini.Entities.Response;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class RegistrationController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        private readonly IGenderService _genderService;
        private readonly ISubjectService _subjectService;
        private readonly ILanguageService _languageService;
        private readonly IQualificationService _qualificationService;

        public RegistrationController(
            IStateService stateService,
            ICountryService countryService,
            IClassService classService,
            IGenderService genderService,
            ISubjectService subjectService,
            ILanguageService languageService,
            IQualificationService qualificationService)
        {
            _classService = classService;
            _stateService = stateService;
            _countryService = countryService;
            _genderService = genderService;
            _subjectService = subjectService;
            _languageService = languageService;
            _qualificationService = qualificationService;
        }

        [HttpGet]
        public Response<RegistrationModel> Get()
            => GetResponse(new RegistrationModel
            {
                Classes = _classService.GetClassData(),
                Genders = _genderService.GetGenderData(),
                Languages = _languageService.GetLanguageData(false),
                Qualifications = _qualificationService.GetQualificationData(),
                Countries= _countryService.GetCountryData(),
                States = _stateService.GetStateData(),
                Subjects = _subjectService.GetSubjectData()
            });
    }
}