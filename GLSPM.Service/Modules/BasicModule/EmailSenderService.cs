using GLSPM.Data.Modules.BasicModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Service.Modules.BasicModule
{
    public class EmailSenderService
    {
        public void SendEmailWithAttached(EmailAccounts emailAccount, string subject, string body, List<EmailAttachment> attachmentList,
            string fromAddress, string fromName, string toAddress, string toName, string bcc, string cc)
        {
            var message = new MailMessage();
            message.From = new MailAddress(fromAddress, fromName);
            if (!string.IsNullOrEmpty(toAddress))
            {
                string[] ToMuliId = toAddress.Split(',');
                foreach (string ToEMailId in ToMuliId)
                {
                    if (!string.IsNullOrEmpty(ToEMailId))
                    {
                        message.To.Add(new MailAddress(ToEMailId.Trim())); //adding multiple TO Email Id
                    }
                }
            }
            //message.To.Add(new MailAddress(toAddress, toName));
            if (!string.IsNullOrEmpty(bcc))
            {
                string[] bccid = bcc.Split(',');
                foreach (string bccEmailId in bccid)
                {
                    if (!string.IsNullOrEmpty(bccEmailId))
                    {
                        message.Bcc.Add(new MailAddress(bccEmailId.Trim())); //Adding Multiple BCC email Id
                    }
                }
            }
            if (!string.IsNullOrEmpty(cc))
            {
                string[] CCId = cc.Split(',');
                foreach (string CCEmail in CCId)
                {
                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        message.CC.Add(new MailAddress(CCEmail.Trim())); //Adding Multiple CC email Id
                    }
                }
            }
            //if (null != bcc)
            //{
            //    foreach (var address in bcc.Where(bccValue => !String.IsNullOrWhiteSpace(bccValue)))
            //    {
            //        message.Bcc.Add(address.Trim());
            //    }
            //}
            //if (null != cc)
            //{
            //    foreach (var address in cc.Where(ccValue => !String.IsNullOrWhiteSpace(ccValue)))
            //    {
            //        message.CC.Add(address.Trim());
            //    }
            //}

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            if(attachmentList.Count > 0)
            {
                foreach(EmailAttachment emailAttachment in attachmentList)
                {
                    message.Attachments.Add(new Attachment(emailAttachment.AttachmentPath));
                }
                
            }
            //Attachment attachment = new Attachment(attachmentPath);
            //message.Attachments.Add(attachment);

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                smtpClient.Host = emailAccount.Host;
                smtpClient.Port = emailAccount.Port;
                smtpClient.EnableSsl = emailAccount.EnableSSL;
                if (emailAccount.UseDefaultCredentials)
                    smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                else
                    smtpClient.Credentials = new NetworkCredential(emailAccount.Username, emailAccount.Password);
                smtpClient.Send(message);
            }
        }

        public void SendBakupFile(EmailAccounts emailAccount, string subject, string body, string attachmentPath,
            string fromAddress, string fromName, string toAddress, string toName, string bcc, string cc)
        {
            var message = new MailMessage();
            message.From = new MailAddress(fromAddress, fromName);
            if (!string.IsNullOrEmpty(toAddress))
            {
                string[] ToMuliId = toAddress.Split(',');
                foreach (string ToEMailId in ToMuliId)
                {
                    if (!string.IsNullOrEmpty(ToEMailId))
                    {
                        message.To.Add(new MailAddress(ToEMailId.Trim())); //adding multiple TO Email Id
                    }
                }
            }
            //message.To.Add(new MailAddress(toAddress, toName));
            if (!string.IsNullOrEmpty(bcc))
            {
                string[] bccid = bcc.Split(',');
                foreach (string bccEmailId in bccid)
                {
                    if (!string.IsNullOrEmpty(bccEmailId))
                    {
                        message.Bcc.Add(new MailAddress(bccEmailId.Trim())); //Adding Multiple BCC email Id
                    }
                }
            }
            if (!string.IsNullOrEmpty(cc))
            {
                string[] CCId = cc.Split(',');
                foreach (string CCEmail in CCId)
                {
                    if (!string.IsNullOrEmpty(CCEmail))
                    {
                        message.CC.Add(new MailAddress(CCEmail.Trim())); //Adding Multiple CC email Id
                    }
                }
            }

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            message.Attachments.Add(new Attachment(attachmentPath));
            //Attachment attachment = new Attachment(attachmentPath);
            //message.Attachments.Add(attachment);

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = emailAccount.UseDefaultCredentials;
                smtpClient.Host = emailAccount.Host;
                smtpClient.Port = emailAccount.Port;
                smtpClient.EnableSsl = emailAccount.EnableSSL;
                if (emailAccount.UseDefaultCredentials)
                    smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                else
                    smtpClient.Credentials = new NetworkCredential(emailAccount.Username, emailAccount.Password);
                smtpClient.Send(message);
            }
        }
    }
}
