using Burgerber.Service.Dtos.Notifications;
using Burgerber.Service.Interfeces.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Burgerber.WepApi.Controllers
{
    [Route("api/sms")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private ISmsSender _smsSender;

        public SmsController(ISmsSender smsSender)
        {
            this._smsSender=smsSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendAsynmc([FromBody] SmsMessange smsMessange)
        {
            return Ok(await _smsSender.SendAsync(smsMessange));
        }
    }
}
