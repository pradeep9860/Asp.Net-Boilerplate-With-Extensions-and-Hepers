using Application.Helpers.MailHelper.Models;
using Microsoft.AspNetCore.Hosting; 
using RazorLight;
using System; 

namespace Application.Helpers.MailHelper
{
     
    public class MailHelper : IMailHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly Application.Helpers.MailHelper.EmailServices.IEmailService _emailService; 

        public MailHelper(IHostingEnvironment hostingEnvironment, Application.Helpers.MailHelper.EmailServices.IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailService = emailService;
        }

        public async void SendEmail(EmailModel<Message> model, string viewPath)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
             
            var engine = new RazorLightEngineBuilder()
              .UseFilesystemProject(contentRootPath)
              .UseMemoryCachingProvider()
              .Build(); 
            try
            {
                string result = await engine.CompileRenderAsync( "/" + viewPath, model.Data);
                var mailModel = new EmailModel
                {
                    To = model.To,
                    Ccs = model.Ccs,
                    Subject = model.Subject,
                    ExtraEmailAddresses = model.ExtraEmailAddresses,
                    Body = "My Test body"
                };

                await _emailService.SendEmail(mailModel);
            }
            catch (Exception)
            { 
            }
        }
    }

    public interface IMailHelper
    {
        void SendEmail(EmailModel<Message> model, string viewPath);
    } 
}
