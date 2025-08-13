using Microsoft.AspNetCore.Mvc;
using Metro_Ticket_Project.Models.DTOs.Card;
using Metro_Ticket_Project.Models.DTOs.Common;
using Metro_Ticket_Project.Services.Interfaces;
using Metro_Ticket_Project.Models.DTOs.User;
using Metro_Ticket_Project.Services.Utilities;

namespace Metro_Ticket_Project.Controllers
{
    [ApiController]
    [Route("api/user/metro_card")]
    public class MetroCardController : ControllerBase
    {
        private readonly IMetroCardService _metroCardService;
        private readonly SendMail _mailService;

        public MetroCardController(IMetroCardService metroCardService, SendMail mailService)
        {
            _metroCardService = metroCardService;
            _mailService = mailService;
        }

        [HttpPost("request_card")]
        public async Task<IActionResult> RequestForMetroCard([FromBody] MetroCardRequest request)
        {
            try
            {
                var card = await _metroCardService.RequestMetroCardAsync(request);

                if (card != null)
                {
                    // Uncomment when mail service is implemented
                    // await _mailService.SendMailAsync(new NotificationEmail 
                    // { 
                    //     Subject = "Metro Card request", 
                    //     To = card.User.Email, 
                    //     Body = "Your MetroCard request has been received successfully !!!" 
                    // });

                    return Ok(new ResponseMessage("Your MetroCard request has been received successfully !!!"));
                }
                else
                {
                    return BadRequest(new ResponseMessage("User already having MetroCard"));
                }
            }
            catch (IOException e)
            {
                return StatusCode(417, new ResponseMessage("Could not proceed your request! Please try again!!!"));
            }
        }

        [HttpPost("card_authenticate")]
        public async Task<IActionResult> CardAuthenticate([FromBody] RechargeRequest request)
        {
            var result = await _metroCardService.CardAuthenticateAsync(request);
            return Ok(result);
        }

        [HttpPut("card_recharge")]
        public async Task<IActionResult> CardRecharge([FromBody] RechargeRequest request)
        {
            var result = await _metroCardService.RechargeCardAsync(request);
            return Ok(result);
        }

        [HttpPost("card")]
        public async Task<IActionResult> CheckBalance([FromBody] UserEmailRequest request)
        {
            var cardDetails = await _metroCardService.FetchCardDetailsAsync(request);
            return Ok(cardDetails);
        }
    }
}