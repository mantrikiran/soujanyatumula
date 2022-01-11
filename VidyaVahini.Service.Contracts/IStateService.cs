using System.Collections.Generic;
using VidyaVahini.Entities.State;

namespace VidyaVahini.Service.Contracts
{
    public interface IStateService
    {
        /// <summary>
        /// Gets the states
        /// </summary>
        StateModel GetStates();

        /// <summary>
        /// Gets the states
        /// </summary>
        IEnumerable<StateData> GetStateData();
    }
}
