// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEmailSenderExtended.cs" company="Innova Marketing Systems S.A.S">
//   © All rights reserved
// </copyright>
// <summary>
//  Email Sender Facede Extended
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace motovalle.Ecommerce.Helpers.EmailSender
{
    using Microsoft.AspNetCore.Identity.UI.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Email sender facede extended
    /// </summary>
    public interface IEmailSenderExtended : IEmailSender
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="htmlMessage">The HTML message.</param>
        /// <param name="attachmentsPath">The attachments path.</param>
        /// <returns>Task for sended email</returns>
        Task SendEmailAsync(string email, string subject, string htmlMessage, List<string> attachmentsPath);

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <param name="attachmentPaths">The attachment paths.</param>
        /// <param name="emailsToBcc">The emails to BCC.</param>
        /// <returns>Task for sended email</returns>
        Task SendEmailAsync(string email, string subject, string bodyMessage, List<string> attachmentPaths, List<string> emailsToBcc);

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <param name="attachmentPaths">The attachment paths.</param>
        /// <param name="emailsToBcc">The emails to BCC.</param>
        /// <param name="emailsToCc">The emails to cc.</param>
        /// <returns>Task for sended email</returns>
        Task SendEmailAsync(string email, string subject, string bodyMessage, List<string> attachmentPaths, List<string> emailsToBcc, List<string> emailsToCc);
    }
}
