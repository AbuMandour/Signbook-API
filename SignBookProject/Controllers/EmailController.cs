using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignbookApi.Models;
using SignbookApi.Services.Interfaces;
using System.Threading.Tasks;

namespace SignbookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailModel model)
        {
            await _emailService.SendEmailAsync(model);
            return Ok();
        }
    }
}
