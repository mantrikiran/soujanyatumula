﻿using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using VidyaVahini.Core.Constant;
using VidyaVahini.Core.Utilities;
using VidyaVahini.Entities.Notification;
using VidyaVahini.Infrastructure.Logger;

namespace VidyaVahini.Services.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public NotificationService(ILogger logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public bool SendEmail(Email email)
        {
            if (email == null || email.To == null ||
                !email.To.Any() || string.IsNullOrEmpty(email.Subject))
                return false;

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(_configuration["Smtp:Server"]);

            mail.From = new MailAddress(
                string.IsNullOrEmpty(email.From) ? _configuration["Smtp:DefaultSender"] : email.From);
            foreach (var recipient in email.To)
            {
                mail.To.Add(new MailAddress(recipient));
            }
            mail.Subject = email.Subject;
            if (string.IsNullOrEmpty(email.Template))
            {
                mail.Body = ApplyReplacements(email.Body, email.Replacements);
            }
            else
            {
                var emailTemplate = File.ReadAllText(email.Template);
                mail.Body = ApplyReplacements(emailTemplate, email.Replacements);
            }
            mail.IsBodyHtml = true;

            SmtpServer.Port = int.Parse(_configuration["Smtp:Port"]);
            SmtpServer.Credentials = new System.Net.NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
            SmtpServer.EnableSsl = bool.Parse(_configuration["Smtp:Ssl"]);

            SmtpServer.Send(mail);
            return true;
        }

        private string ApplyReplacements(string emailBody, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(emailBody))
                return string.Empty;

            if (replacements == null)
                replacements = new Dictionary<string, string>();

            if (!replacements.ContainsKey(Constants.HostReplacement))
                replacements.Add(Constants.HostReplacement, _configuration["Application:Host"]);

            if (!replacements.ContainsKey(Constants.ApiVersionReplacement))
                replacements.Add(Constants.ApiVersionReplacement, _configuration["Application:ApiVersion"]);

            if (!replacements.ContainsKey(Constants.AppHostReplacement))
                replacements.Add(Constants.AppHostReplacement, _configuration["Application:AppHost"]);

            return NotificationUtil.ApplyReplacements(emailBody, replacements);
        }
    }
}
