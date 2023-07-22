using Burgerber.Service.Dtos.Notifications;
using Burgerber.Service.Interfeces.Notifications;

namespace Burgerber.Service.Services.Notifications;

public class SmsSender : ISmsSender
{
    public Task<bool> SendAsync(SmsMessange smsMessange)
    {
        throw new NotImplementedException();
    }
}
