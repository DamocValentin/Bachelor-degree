using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using FFG.Services;

namespace FFG.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        public static Task SendRequestApprovedConfirmationAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Participation Request Status",
                $"Your request to participate to the activity was approved. Have fun!");
        }

        public static Task SendRequestDeclinedConfirmationAsync(this IEmailSender emailSender, string email)
        {
            return emailSender.SendEmailAsync(email, "Participation Request Status",
                $"Your request to participate to the activity was not approved.");
        }
    }
}
