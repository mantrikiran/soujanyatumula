using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Enum;
using VidyaVahini.Entities.Notification;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Infrastructure.Exception;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IErrorRepository _errorRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IConfiguration _configuration;

        public UserAccountService(
            IErrorRepository errorRepository,
            IUserAccountRepository userAccountRepository,
            INotificationRepository notificationRepository,
            IConfiguration configuration)
        {
            _errorRepository = errorRepository;
            _userAccountRepository = userAccountRepository;
            _notificationRepository = notificationRepository;
            _configuration = configuration;
        }
       


        public void ActivateAccount(TokenCommand token)
        {
            if(!_userAccountRepository.ActivateAccount(token.Token))
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TokenInvalidOrExpired));
        }

        public UserModel AuthenticateUser(LoginCommand login)
            => _userAccountRepository.AuthenticateUser(login.Username, login.Password);


        public MetorData MentorProfile(MentorProfileCommand mpc)
          => _userAccountRepository.GetMentorProfile(mpc.TeacherId);

        public void ForgotPassword(ForgotPasswordCommand forgotPassword)
        {
            var userDetails = _userAccountRepository.ForgotPassword(forgotPassword.Username, Constants.TokenExpiry);

            if (userDetails != null)
            {
                var sent = _notificationRepository.SendEmail(new Email
                {
                    Replacements = new Dictionary<string, string>
                    {
                        { Constants.NameReplacement, userDetails.Name },
                        { Constants.EmailReplacement, userDetails.Email },
                        { Constants.TokenReplacement, userDetails.Token },
                        { Constants.TokenExpiryReplacement, Convert.ToString(Constants.TokenExpiry) }                      
                       
                    },
                    Subject = Constants.ForgotPasswordEmailSubject,
                    Template = Constants.ForgotPasswordEmailTemplate,
                    To = new List<string> { userDetails.Email }
                });

                if (!sent)
                    throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.ErrorSendingNotification));
            }
            else
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.Notaregistereduser));

            }
        }

        public void TicketEmail(TicketCommand ticket)
        {
            int c = ticket.file.ToList<string>().Count;
            List<string> pathlists = new List<string>();
            string fullOutputPath = null;
            string root = null;
            root = @"C:\PYF\MailAttachment\" + ticket.Email;           
            for (int i = 0; i < c; i++)
            {
                var myList = ticket.file.ToList();
                var imgres = myList.ElementAt(i);
                string result = imgres.Substring(0, imgres.IndexOf(","));
                int Pos1 = result.IndexOf("data:image/") + "data:image/".Length;
                int Pos2 = result.IndexOf(";");
               string FormatImg = result.Substring(Pos1, Pos2 - Pos1);
                string convert = imgres.Replace(result+",", String.Empty); 
                // If directory does not exist, create it. 
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                fullOutputPath =root+ @"\"+"img" + i + "."+ FormatImg;
                System.Drawing.Image img = Base64ToImage(convert);
                img.Save(fullOutputPath); 
                pathlists.Add(fullOutputPath); 
            }

            var sent = _notificationRepository.SendSupUserEmail(new Email
            {
                Replacements = new Dictionary<string, string>
                    {
                        { Constants.Description, ticket.Description },
                        { Constants.EmailReplacement, ticket.Email },
                        { Constants.Date,DateTime.Now.ToString() }
                    },
                Subject = Constants.SupportEmailSubject,
                Template = Constants.SupportEmailTemplate,
                To = new List<string> { _configuration["Smtp:To"] },               
                ImagePath=pathlists
            });
            var sent1 = _notificationRepository.SendSupUserEmail(new Email
            {
                Replacements = new Dictionary<string, string>
                    {
                      { Constants.EmailReplacement, ticket.Email }
                    },
                Subject = Constants.UserEmailSubject,
                Template = Constants.UserEmailTemplate,
                To = new List<string> { ticket.Email },             
                ImagePath = pathlists
            }); 
            
            if (!sent && !sent1)
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.ErrorSendingNotification));

            int cnt = pathlists.Count;
            for (int i = 0; i < cnt; i++)
            {
                string path = pathlists.ElementAt(i);                
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            Directory.Delete(root);
        }

        public System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);          
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);          
            return image;
          
        }

        public void ChangePassword(ChangePasswordCommand changePassword)
        {
            if(!_userAccountRepository.ChangePassword(changePassword.Username, changePassword.Password))
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.UserAccountNotFound));
        }

        public UserBasicDetailsModel ValidateToken(TokenCommand token)
        {
            var userDetails = _userAccountRepository.ValidateToken(token.Token);

            if (userDetails == null)
            {
                throw new VidyaVahiniException(_errorRepository.GetError((int)Enums.Error.TokenInvalidOrExpired));
            }

            return userDetails;
        }
    }
}
