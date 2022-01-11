using System.Collections.Generic;
using VidyaVahini.Entities.State;

namespace VidyaVahini.Repository.Contracts
{
    public interface IStateRepository
    {
        /// <summary>
        /// Gets all the states from the database
        /// </summary>
        IEnumerable<StateData> GetStateData();
    }
}
