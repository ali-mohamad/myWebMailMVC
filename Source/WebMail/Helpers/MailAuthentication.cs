using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using ImapX;
namespace myWebMail.Helpers
{
    internal class MailAuthentication
    {
        internal  bool GmailAuthenticate(string username, string password)
        {
            var client = new ImapClient("imap.gmail.com", 993, true, true);
            client.Connect();
            bool result = client.Login(username,password);
            if (result) return true;
            else return false;
        }
    } 
}