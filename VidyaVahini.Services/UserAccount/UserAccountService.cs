using VidyaVahini.Core.Enum;
using VidyaVahini.Infrastructure.Exception;
using VidyaVahini.Repository.DataAccess;
using VidyaVahini.Repository.Error;
using VidyaVahini.Repository.UserAccount;

namespace VidyaVahini.Services.UserAccount
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorRepository _errorRepository;
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(
            IUnitOfWork unitOfWork,
            IErrorRepository errorRepository,
            IUserAccountRepository userAccountRepository)
        {
            _unitOfWork = unitOfWork;
            _errorRepository = errorRepository;
            _userAccountRepository = userAccountRepository;
        }

        public bool ActivateAccount(string token)
        {
            if(!_userAccountRepository.ActivateAccount(token))
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TokenInvalidOrExpired));

            var recordsUpdated = _unitOfWork.Commit();

            return recordsUpdated > 0;
        }
    }
}
