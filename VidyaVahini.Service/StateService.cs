using System.Collections.Generic;
using VidyaVahini.Entities.State;
using VidyaVahini.Infrastructure.Contracts;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly ILogger _logger;

        public StateService(ILogger logger, IStateRepository stateRepository)
        {
            _logger = logger;
            _stateRepository = stateRepository;
        }

        public IEnumerable<StateData> GetStateData()
            => _stateRepository.GetStateData();

        public StateModel GetStates()
            => new StateModel
            {
                States = GetStateData()
            };
    }
}
