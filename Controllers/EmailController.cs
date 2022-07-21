using EmailSender.Interfaces;
using EmailSender.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IMailService _email;

        public EmailController(IMailService email)
        {
            _email = email;
        }

        [HttpPost("send-customer-mail")]
        public async Task<IActionResult> SendMailToCustomer(CustomersDTO customer)
        {
            try
            {
                var response = await _email.SendMailToCustomers(customer);
                return Ok(response);
            }
            catch(Exception e)
            {
                return BadRequest("Error occured while processing your request: " + e.Message + " : " + e.ToString());
            }
        }
    }
}