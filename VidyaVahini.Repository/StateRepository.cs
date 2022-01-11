using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.State;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StateData> GetStateData()
            => _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.State>()
                .GetAll()
                .Select(x => new StateData()
                {
                    Id = x.StateId,
                    Name = x.StateName
                });
    }
}
