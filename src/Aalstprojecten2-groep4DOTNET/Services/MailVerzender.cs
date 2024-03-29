﻿using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit.Utils;

namespace Aalstprojecten2_groep4DOTNET.Services
{
    public class MailVerzender
    {
        public static void VerzendMailEersteKeerInloggen(string naam, string email, string wachtwoord)
        {
            BodyBuilder builder = new BodyBuilder();
            builder.HtmlBody = "<!DOCTYPE html><html><body style='background-color: rgb(234, 240, 242);align-content: center;font-family: -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Oxygen, Ubuntu, Cantarell, Fira Sans, Droid Sans, Helvetica Neue, sans-serif;font-weight: 100;'><div style='text-align: center;'><img src='http://users.hogent.be/~533726pp/eportfolio/signature/icon.png' alt='LOGO' class='logo' style='padding-top: 50px;max-height: 40px; max-width: 40px;'><p id='aanspreking' style='text-aling: center;font-size: 2em;font-weight: 100;'>Beste <strong>" + naam + "</strong>.</p><p style='padding-left: 100px;padding-right: 100px;'></p></div><div style='text-align: center;display: block;margin: 0 auto;min-height: 400px;max-width: 600px;background-color: white;'><img src='http://users.hogent.be/~533726pp/eportfolio/signature/email.png' alt='PRENT' style='padding-top: 50px;'><p>Leuk dat je gebruik wil maken van onze tool om werkgevers meer inzicht te geven in de kosten en baten bij het tewerkstellen van personen met een grote afstand tot de arbeidsmarkt.</p><hr style='margin-top: 40px;display: block;margin-left: auto;margin-right: auto;border-width: 1px;border-color: white;max-width: 50%;'></hr><p>Je kan nu inloggen op Kairos met deze gebruikersnaam en paswoord:</p><p>Gebruikersnaam: <strong>" + email + "</strong></p><p>Paswoord: <strong>" + wachtwoord + "</strong></p><p>Na het inloggen kan je je paswoord veranderen.</p><a href='http://localhost:1043/' style='width: 250px;height: 40px;background-color: rgb(23, 146, 219);border-radius: 5px;color: white;font-size: 1.2em;border: none;text-decoration: none;font-weight: 70;margin-top: 20px;margin-bottom: 30px;padding: 10px;'>Inloggen</a></div><div class='bottom2' style='text-align: center;display: block;margin: 0 auto;max-width: 600px;background-color: white;'><p class='stand' style='text-align:center; padding-top: 40px;'>Veel succes met het gebruik van onze tool! Wil je meer weten over wie we zijn en wat we doen, surf naar <a href='https://www.hetmomentvooriedereen.nu' style='color: rgb(23, 146, 219);text-decoration: none;'>www.hetmomentvooriedereen.nu</a>.</p><p id='laatste' style='text-align: center;padding-bottom: 30px;'>Hartelijke groet Het team van KAIROS</p></div></body></html>";

            VerzendMailMetBody(naam, email, builder, "Eerste keer inloggen").Wait();
        }

        public static void VerzendMailWachtwoordVergeten(string naam, string email, string callbackUrl)
        {
            BodyBuilder builder = new BodyBuilder();
            builder.HtmlBody = "<!DOCTYPE html><html><body style='background-color: rgb(234, 240, 242);align-content: center;font-family: -apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Oxygen, Ubuntu, Cantarell, Fira Sans, Droid Sans, Helvetica Neue, sans-serif;font-weight: 100;'><div style='text-align: center;'><img src='http://users.hogent.be/~533726pp/eportfolio/signature/icon.png' alt='LOGO' class='logo' style='padding-top: 50px;max-height: 40px; max-width: 40px;'><p id='aanspreking' style='text-align: center;font-size: 2em;font-weight: 100;'>Beste <strong>" + naam + "</strong>.</p><p style='padding-left: 100px;padding-right: 100px;'></p></div><div style='text-align: center;display: block;margin: 0 auto;min-height: 400px;max-width: 600px;background-color: white;'><img src='http://users.hogent.be/~533726pp/eportfolio/signature/email.png' alt='PRENT' style='padding-top: 50px;'><p>Leuk dat je gebruik wil maken van onze tool om werkgevers meer inzicht te geven in de kosten en baten bij het tewerkstellen van personen met een grote afstand tot de arbeidsmarkt.</p><hr style='margin-top: 40px;display: block;margin-left: auto;margin-right: auto;border-width: 1px;border-color: white;max-width: 50%;'></hr><p>Je kan nu inloggen op Kairos met deze <a href='" + callbackUrl + "'>link</a></div><div class='bottom2' style='text-align: center;display: block;margin: 0 auto;max-width: 600px;background-color: white;'><p class='stand' style='text-align: center; padding-top: 40px;'>Veel succes met het gebruik van onze tool! Wil je meer weten over wie we zijn en wat we doen, surf naar <a href='https://www.hetmomentvooriedereen.nu' style='color: rgb(23, 146, 219);text-decoration: none;'>www.hetmomentvooriedereen.nu</a>.</p><p id='laatste' style='text-align: center;padding-bottom: 30px;'>Hartelijke groet Het team van KAIROS</p></div></body></html>";

            VerzendMailMetBody(naam, email, builder, "Wachtwoord vergeten").Wait();
            
        }

        public static async Task ContacteerAdmin(string naam, string email, string onderwerp, string inhoud)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(email, email));
            message.To.Add(new MailboxAddress("Admin", "andreas.dewitte@hotmail.com"));
            message.Subject = onderwerp;

            message.Body = new TextPart("plain")
            {
                Text = inhoud
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

        public static async Task StuurPdf(string naam, string email, string ontvangerEmail, string onderwerp, string inhoud, string attachment)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(naam, email));
            message.To.Add(new MailboxAddress(ontvangerEmail, ontvangerEmail));
            message.Subject = onderwerp;

            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = inhoud;
            builder.Attachments.Add(attachment);

            message.Body = builder.ToMessageBody();



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

        //string body moet eventueel veranderen naar BodyBuilder object voor in de body te steken
        private static async Task VerzendMailMetBody(string naam, string email, BodyBuilder body, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Kairos ITSolutions", "ITSolutions.Kairos@gmail.com"));
            message.To.Add(new MailboxAddress(naam, email));
            message.Subject = subject;

            message.Body = body.ToMessageBody();

            //message.Body = new TextPart("plain")
            //{
            //    Text = body
            //};

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
