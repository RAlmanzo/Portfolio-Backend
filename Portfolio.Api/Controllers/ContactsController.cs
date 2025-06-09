using Microsoft.AspNetCore.Mvc;
using Portfolio.Api.Dtos;
using Portfolio.Core.Interfaces.Services;
using Portfolio.Core.Services.Models;

namespace Portfolio.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ContactsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailAsync([FromForm] EmailRequestDto emailRequestDto)
        {
            var result = await _emailService.SendEmailAsync
            (
                new EmailCreateRequestModel
                {
                    From = emailRequestDto.From,
                    Subject = emailRequestDto.Subject,
                    Message = emailRequestDto.Message,
                }
            );

            if (result.Success)
                return Ok(new { Message = result.Value });

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
            return BadRequest(ModelState);
        }
    }
}
