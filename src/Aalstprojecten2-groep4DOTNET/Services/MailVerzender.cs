using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace Aalstprojecten2_groep4DOTNET.Services
{
    public class MailVerzender
    {
        public static void VerzendMailEersteKeerInloggen(string naam, string email, string wachtwoord)
        {
            
            VerzendMailMetBody(naam, email, wachtwoord, "Eerste keer inloggen").Wait();
        }

        public static void VerzendMailWachtwoordVergeten(string naam, string email, string callbackUrl)
        {
            VerzendMailMetBody(naam, email, callbackUrl, "Wachtwoord vergeten").Wait();
            
        }

        //string body moet eventueel veranderen naar BodyBuilder object voor in de body te steken
        private static async Task VerzendMailMetBody(string naam, string email, string body, string subject)
        {
            //BodyBuilder builder = new BodyBuilder();
            //builder.
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Kairos ITSolutions", "ITSolutions.Kairos@gmail.com"));
            message.To.Add(new MailboxAddress(naam, email));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.gmail.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("ITSolutions.Kairos@gmail.com", "ITSolutions123");

                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
