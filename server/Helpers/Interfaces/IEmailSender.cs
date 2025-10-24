using System;

namespace server.Helpers.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string bodyHtml);
}
