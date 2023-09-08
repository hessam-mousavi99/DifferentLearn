using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DifferentLearn.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string to,string subject,string body)
        {
            MailMessage mail=new MailMessage();
            SmtpClient smtpServer=new SmtpClient("smtp.gmail.com",587);
            mail.From = new MailAddress("hessamSoftware99@gmail.com", "Different Learn");
            mail.To.Add(to);
            mail.Subject= subject;
            mail.Body= body;
            mail.IsBodyHtml= true;
            smtpServer.EnableSsl = true;
            //smtpServer.Port = 587;//465v587
            smtpServer.Credentials = new System.Net.NetworkCredential("hessamSoftware99@gmail.com", "snghpusiuugyepub");     
            smtpServer.Send(mail);
        }
    }
}
