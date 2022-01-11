using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserAccountService _userAccountService;

        public AccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> Activate([FromBody] TokenCommand token)
        {
            _userAccountService.ActivateAccount(token);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> ForgotPassword([FromBody] ForgotPasswordCommand forgotPassword)
        {
            _userAccountService.ForgotPassword(forgotPassword);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> TicketEmail([FromBody] TicketCommand ticket)
        {           
            _userAccountService.TicketEmail(ticket);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<SuccessModel> ChangePassword([FromBody] ChangePasswordCommand changePassword)
        {
            _userAccountService.ChangePassword(changePassword);

            return GetResponse(new SuccessModel
            {
                Success = true
            });
        }

        [Route("[action]")]
        [HttpPost]
        public Response<UserBasicDetailsModel> ValidateToken([FromBody] TokenCommand token)
            => GetResponse(_userAccountService.ValidateToken(token));
    }
}