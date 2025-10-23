using System;
using MailKit.Net.Smtp;
using MimeKit;
using server.Helpers.Interfaces;
namespace server.Helpers;

public class EmailSender : IEmailSender
{

    public async Task SendEmailAsync(string to, string subject, string bodyHtml)
    {
        var myEmail = "edvaldojoao520@gmail.com";
        var myEmailPassword = "eqjv hrjy drwf somk";

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("NoReply", myEmail));
        message.To.Add(new MailboxAddress("", to));
        message.Subject = subject;

        // Corpo em HTML
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
        <html>
            <body style='font-family: Arial, sans-serif;'>
                <h2 style='color: #2E86C1;'>Olá! aqui está o seu código de confirmação para votar <br>
                Não compartilhe este código com ninguém.
                </h2>
                <p style='font-size: 2em;'><b>{bodyHtml}</b></p>
                <hr>
                <p style='font-size: 0.9em; color: #888;'>Este é um email automático, não responda.</p>
            </body>
        </html>"
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(myEmail, myEmailPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }

}