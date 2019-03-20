using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Application.Helpers.MailHelper.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration; 

namespace Application.Helpers.MailHelper.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment; 

        public EmailService(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment; 
        }

        public async Task SendEmail(EmailModel mailInput)
        {
            try
            {
                var userEmail = _configuration["EmailConfig:UserEmailAddress"];
                var userPassword = _configuration["EmailConfig:UserPassword"];
                var host = _configuration["EmailConfig:Host"];
                var port = _configuration["EmailConfig:Port"];
                var enableSSL = _configuration["EmailConfig:EnableSSL"];
                var defaultCCEmails = _configuration["EmailConfig:DefaultCCEmails"];
                var defaultEmails = _configuration["EmailConfig:DefaultEmails"];
                var defaultSubject = _configuration["EmailConfig:DefaultSubject"]; 

                using (var client = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = userEmail,
                        Password = userPassword
                    };

                    client.Credentials = credential;
                    client.Host = host;
                    client.EnableSsl = Convert.ToBoolean(enableSSL);

                    //Try port 587 instead of 465. Port 465 is technically deprecated.
                    client.Port = Convert.ToInt32(port);

                    using (var emailMessage = new MailMessage())
                    {
                        emailMessage.To.Add(mailInput.To); 
                        foreach (var email in mailInput.ExtraEmailAddresses)
                        {
                            emailMessage.To.Add(email);
                        }
                       
                        emailMessage.From = new MailAddress(userEmail);

                        if (_environment.IsDevelopment())
                        {
                            foreach (var ccEmail in defaultEmails.Split(','))
                            {
                                emailMessage.CC.Add(ccEmail);
                            }

                            foreach (var ccEmail in defaultCCEmails.Split(','))
                            {
                                emailMessage.CC.Add(ccEmail);
                            }
                        }
                        else
                        {
                            foreach (var ccEmail in mailInput.Ccs)
                            {
                                emailMessage.CC.Add(ccEmail);
                            }
                        }

                        emailMessage.Subject = string.IsNullOrEmpty(mailInput.Subject)? defaultSubject : mailInput.Subject;
                        emailMessage.Body = mailInput.Body;  
                        emailMessage.IsBodyHtml = true;

                        // Include "Message-Id" header or your message will be treated as spam by Google.
                        emailMessage.Headers.Add("Message-Id", String.Concat("<", DateTime.Now.ToString("yyMMdd"), ".", DateTime.Now.ToString("HHmmss"), "@amniltech.com"));

                        client.Send(emailMessage);
                    }
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
    }
}
