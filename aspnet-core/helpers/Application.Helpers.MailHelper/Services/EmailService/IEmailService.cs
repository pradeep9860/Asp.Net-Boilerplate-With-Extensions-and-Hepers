

using Application.Helpers.MailHelper.Models;
using System.Threading.Tasks;

namespace Application.Helpers.MailHelper.EmailServices
{
    public interface IEmailService
    {
        Task SendEmail(EmailModel mailInput);
    }
}
