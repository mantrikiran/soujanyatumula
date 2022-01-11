using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.Entities.Response;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class ErrorRepository : IErrorRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public ErrorRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ErrorDetails GetError(int errorId)
        {
            var erroDetails = _unitOfWork
                .Repository<VidyaVahini.DataAccess.Models.Error>()
                .Find(x => x.ErrorId == errorId);

            return new ErrorDetails
            {
                Code = erroDetails?.ErrorCode,
                Message = erroDetails?.ErrorMessage
            };
        }
    }
}
