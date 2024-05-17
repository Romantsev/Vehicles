using System.Net;
using System.Net.Mail;
using BLL.Interfaces;
using Core;
using Core.Helpers;
using Core.Models;
using Microsoft.Extensions.Options;

namespace BLL.Services;

public class MailService : IMailService
{
    private readonly MailSettings _settings;

    public MailService(IOptions<MailSettings> settings)
    {
        _settings = settings.Value;
    }

    public void SendEmail(Operation operation, string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            
            var mailMessage = new MailMessage();
       
            mailMessage.From = new MailAddress(_settings.From);
       
            mailMessage.To.Add(mailAddress);
       
            mailMessage.Subject = $"Information about your operation with vehicle.";

            var body = $"Operation code: {operation.OperationCode}\n" +
                       $"Operation name: {operation.OperationName}\n" +
                       $"New regestration number: {operation.NumberRegNew}";
            
            mailMessage.Body = body;
       
            using var smtpClient = new SmtpClient(_settings.Host)
            {
                Port = _settings.Port,
                Credentials = new NetworkCredential(_settings.From, _settings.Password),
                EnableSsl = _settings.UseSSL
            };

            smtpClient.Send(mailMessage);
        }
        catch (FormatException)
        {
        }
    }
}