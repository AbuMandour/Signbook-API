using SignbookApi.Models;
using System.Threading.Tasks;

namespace SignbookApi.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel model);
    }
}
