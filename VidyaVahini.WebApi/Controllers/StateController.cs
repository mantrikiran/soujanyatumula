using Microsoft.AspNetCore.Mvc;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.State;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class StateController : BaseController
    {
        private readonly IStateService _stateService;

        public StateController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet]
        public Response<StateModel> Get()
            => GetResponse(_stateService
                .GetStates());
    }
}