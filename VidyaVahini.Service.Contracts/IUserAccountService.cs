using VidyaVahini.Entities.UserAccount;

namespace VidyaVahini.Service.Contracts
{
    public interface IUserAccountService
    {
        /// <summary>
        /// Validates the token, disables it and marks the user account as active
        /// </summary>
        /// <param name="token">Token</param>
        void ActivateAccount(TokenCommand token);

        /// <summary>
        /// Validates user credentials
        /// </summary>
        /// <param name="login">User credentials</param>
        /// <returns>Basic user profile details</returns>
        UserModel AuthenticateUser(LoginCommand login);

        /// <summary>
        /// Validates if an user account is active and sends out an email to reset account password
        /// </summary>
        /// <param name="forgotPassword">username</param>
        void ForgotPassword(ForgotPasswordCommand forgotPassword);

        /// <summary>
        /// Updates user account password
        /// </summary>
        /// <param name="changePassword">updated credentials</param>
        void ChangePassword(ChangePasswordCommand changePassword);
        void TicketEmail(TicketCommand ticket);

        /// <summary>
        /// Validates a token and disables it
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>user basic details</returns>
        UserBasicDetailsModel ValidateToken(TokenCommand token);
        MetorData MentorProfile(MentorProfileCommand mpc);
    }
}
