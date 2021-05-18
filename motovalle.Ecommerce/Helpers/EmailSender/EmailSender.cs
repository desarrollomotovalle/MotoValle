// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Email Sender helper
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.EmailSender
{
    using Microsoft.Extensions.Configuration;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Email Sender helper
    /// </summary>
    public class EmailSender : IEmailSenderExtended
    {
        /// <summary>
        /// The host
        /// </summary>
        private readonly string _host;

        /// <summary>
        /// The port
        /// </summary>
        private readonly int _port;

        /// <summary>
        /// The enable SSL
        /// </summary>
        private readonly bool _enableSSL;

        /// <summary>
        /// The user name
        /// </summary>
        private readonly string _emailFrom;

        /// <summary>
        /// The password
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// The default subject
        /// </summary>
        private readonly string _defaultSubject;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public EmailSender(IConfiguration configuration)
        {
            this._host = configuration.GetSection("EmailSettings").GetValue<string>("Host");
            this._port = configuration.GetSection("EmailSettings").GetValue<int>("Port");
            this._enableSSL = configuration.GetSection("EmailSettings").GetValue<bool>("EnableSSL");
            this._emailFrom = configuration.GetSection("EmailSettings").GetValue<string>("EmailFrom");
            this._password = configuration.GetSection("EmailSettings").GetValue<string>("Password");
            this._defaultSubject = configuration.GetSection("EmailSettings").GetValue<string>("DefaultSubject");
        }

        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <returns>Task to Send the message</returns>
        public Task SendEmailAsync(string email, string subject, string bodyMessage)
        {
            var smtpClient = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(this._emailFrom, this._password),
                EnableSsl = this._enableSSL
            };

            return smtpClient.SendMailAsync(
                    new MailMessage(this._emailFrom, email, subject ?? this._defaultSubject, bodyMessage)
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8
                    });
        }

        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <param name="attachmentPath">The attachment path.</param>
        /// <returns>
        /// Task to Send the message
        /// </returns>
        public Task SendEmailAsync(string email, string subject, string bodyMessage, List<string> attachmentPaths)
        {
            var smtpClient = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(this._emailFrom, this._password),
                EnableSsl = this._enableSSL
            };

            var message = new MailMessage(this._emailFrom, email, subject ?? this._defaultSubject, bodyMessage)
            {
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };

            if (attachmentPaths != null)
            {
                foreach (var item in attachmentPaths)
                {
                    var data = new Attachment(item);
                    message.Attachments.Add(data);
                }
            }

            return smtpClient.SendMailAsync(message);
        }

        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <param name="emailToBcc">The email to BCC.</param>
        /// <returns>
        /// Task to Send the message
        /// </returns>
        public Task SendEmailAsync(string email, string subject, string bodyMessage, List<string> attachmentPaths, List<string> emailsToBcc)
        {
            var smtpClient = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(this._emailFrom, this._password),
                EnableSsl = this._enableSSL
            };

            var message = new MailMessage(this._emailFrom, email, subject ?? this._defaultSubject, bodyMessage)
            {
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };

            if (attachmentPaths != null)
            {
                foreach (var item in attachmentPaths)
                {
                    var data = new Attachment(item);
                    message.Attachments.Add(data);
                }
            }

            if (emailsToBcc != null)
            {
                foreach (var item in emailsToBcc)
                {
                    var data = new MailAddress(item);
                    message.Bcc.Add(data);
                }
            }

            return smtpClient.SendMailAsync(message);
        }

        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <param name="attachmentPaths">The attachment paths.</param>
        /// <param name="emailsToBcc">The emails to BCC.</param>
        /// <param name="emailsToCc">The emails to cc.</param>
        /// <returns>
        /// Task to Send the message
        /// </returns>
        public Task SendEmailAsync(string email, string subject, string bodyMessage, List<string> attachmentPaths, List<string> emailsToBcc, List<string> emailsToCc)
        {
            var smtpClient = new SmtpClient(this._host, this._port)
            {
                Credentials = new NetworkCredential(this._emailFrom, this._password),
                EnableSsl = this._enableSSL
            };

            var message = new MailMessage(this._emailFrom, email, subject ?? this._defaultSubject, bodyMessage)
            {
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8
            };

            if (attachmentPaths != null)
            {
                foreach (var item in attachmentPaths)
                {
                    var data = new Attachment(item);
                    message.Attachments.Add(data);
                }
            }

            if (emailsToBcc != null)
            {
                foreach (var item in emailsToBcc)
                {
                    var data = new MailAddress(item);
                    message.Bcc.Add(data);
                }
            }

            if (emailsToCc != null)
            {
                foreach (var item in emailsToCc)
                {
                    var data = new MailAddress(item);
                    message.CC.Add(data);
                }
            }

            return smtpClient.SendMailAsync(message);
        }
    }
}
