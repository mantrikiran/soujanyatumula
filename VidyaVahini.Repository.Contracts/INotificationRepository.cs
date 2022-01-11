using VidyaVahini.Entities.Notification;

namespace VidyaVahini.Repository.Contracts
{
    public interface INotificationRepository
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="email">Email details</param>
        /// <returns>success/failure</returns>
        bool SendEmail(Email email);
        bool SendSupUserEmail(Email email);
    }
}
