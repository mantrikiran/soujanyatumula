using VidyaVahini.Entities.Notification;

namespace VidyaVahini.Services.Notification
{
    public interface INotificationService
    {
        bool SendEmail(Email email);
    }
}
