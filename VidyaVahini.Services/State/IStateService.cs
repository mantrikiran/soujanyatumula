using System.Collections.Generic;
using VidyaVahini.Entities.State;

namespace VidyaVahini.Services.State
{
    public interface IStateService
    {
        StateModel GetStates();

        IEnumerable<StateData> GetStateData();
    }
}
