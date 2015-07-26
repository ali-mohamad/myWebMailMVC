using myWebMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ImapX;
using System.Configuration;
using MailSystem = ActiveUp.Net.Mail;

namespace myWebMail.Helpers
{
    internal class MailOperations
    {
        internal async Task<List<MailItem>> GetEmailMessages(string user,string pass,int pageNo, int pageSize)
        {
            List<MailItem> returnResults = new List<MailItem>();
            var client = new ImapClient("imap.gmail.com", 993,true, true);
            client.Connect();
            client.Login(user,pass);
            var messages = client.Folders["INBOX"].Search("ALL",ImapX.Enums.MessageFetchMode.Basic| ImapX.Enums.MessageFetchMode.Flags, pageSize);
            foreach (ImapX.Message msg in messages)
            {
                MailItem item = new MailItem();
                item.Sender = msg.From.ToString();
                item.Body = msg.Body.Text;
                item.Subject = msg.Subject;
                item.Received = msg.Date;
                
                returnResults.Add(item);
                
            }
            return returnResults;
        }

        internal async Task<string> ComposeAndSendMailAsync(string user,
                                                            string pass,
                                                            string subject,
                                                           string bodyContent,
                                                           string recipients)
        {
            MailSystem.Message message = new MailSystem.Message();
            message.From = new MailSystem.Address(user);
            string[] toEmails = recipients.Split(';');
            foreach (string mailRecepient in toEmails)
            {
                message.To.Add(mailRecepient);
            }
            message.Subject = subject;

            //message.BodyHtml.Text = "This is some html <b>content</b>";
            message.BodyText.Text = bodyContent;
            bool result = ActiveUp.Net.Mail.SmtpClient.SendSsl(message,
               ConfigurationManager.AppSettings["GmailHost"],
               int.Parse(ConfigurationManager.AppSettings["GmailPort"]),
               user,
               pass,
               MailSystem.SaslMechanism.Login);

            if (result) return "1";
            return "-1";
        }
    }
}