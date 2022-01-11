namespace VidyaVahini.Services.UserAccount
{
    public interface IUserAccountService
    {
        bool ActivateAccount(string token);
    }
}
