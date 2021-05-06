using SharedEntities;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace SharedServices
{
    public class EmailServices
    {
        public void SendEmail(EmailQueue pModel)
        {
            string _message = string.Empty;

            MailMessage _mailMessage = new MailMessage();

            SmtpClient _smtpClient = new SmtpClient();

            NetworkCredential _networkCredential = new NetworkCredential();

            try
            {
                _mailMessage.To.Add(new MailAddress(pModel.EmailTo));
                _mailMessage.From = new MailAddress(ConfigurationManager.AppSettings.Get("SMTPUser"));
                _mailMessage.Subject = pModel.Subject;
                _mailMessage.Body = pModel.Body;
                _mailMessage.IsBodyHtml = true;

                _networkCredential.UserName = ConfigurationManager.AppSettings.Get("SMTPUser");
                _networkCredential.Password = ConfigurationManager.AppSettings.Get("SMTPSecure");

                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = _networkCredential;
                _smtpClient.Host = ConfigurationManager.AppSettings.Get("SMTPServer");
                _smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings.Get("SMTPPort"));
                _smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("SMTPSSL"));

                //Attachment _attachment = new Attachment()
                //_mailMessage.Attachments.Add()

                _smtpClient.Send(_mailMessage);
            }
            catch (Exception)
            {
                //TODO: enqueue the email for future send
                throw;
            }
        }
    }
}
