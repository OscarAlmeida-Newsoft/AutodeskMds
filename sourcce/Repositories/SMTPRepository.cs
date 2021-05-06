using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRepositories;
using System.Net.Mail;
using System.Net;

namespace Repositories
{
    public class SMTPRepository : ISMTPRepository
    {
        ISMTPConnectionProvider SMTPConnectionProvider;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pSMTPConnectionProvider"></param>
        public SMTPRepository(ISMTPConnectionProvider pSMTPConnectionProvider)
        {
            SMTPConnectionProvider = pSMTPConnectionProvider;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTo"></param>
        /// <param name="pSubject"></param>
        /// <param name="pBody"></param>
        public string EnviarCorreo(string pTo, string pSubject, string pBody)
        {
            string mensaje = "";
            MailMessage message = new MailMessage();

            SmtpClient smtpClient = new SmtpClient();

            NetworkCredential networkCredential = new NetworkCredential();

            try
            {
                MailAddress _smtpUser = new MailAddress(SMTPConnectionProvider.GetUserConnection());

                message.To.Add(new MailAddress(pTo));
                message.Bcc.Add(_smtpUser);
                message.From = _smtpUser;
                message.Subject = pSubject;
                message.Body = pBody;
                message.IsBodyHtml = true;

                networkCredential.UserName = SMTPConnectionProvider.GetUserConnection();
                networkCredential.Password = SMTPConnectionProvider.GetSecureConnection();

                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Host = SMTPConnectionProvider.GetSMTPServerConnection();
                smtpClient.Port = Convert.ToInt32(SMTPConnectionProvider.GetPortConnection());
                smtpClient.EnableSsl = Convert.ToBoolean(SMTPConnectionProvider.GetSSLConnection());

                smtpClient.Send(message);
            }
            catch (SmtpException ex)
            {
                mensaje = SMTPConnectionProvider.GetUserConnection();
            }


            return mensaje;
        }
    }
}
