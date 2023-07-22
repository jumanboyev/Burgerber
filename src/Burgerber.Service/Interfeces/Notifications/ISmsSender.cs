using Burgerber.Service.Dtos.Notifications;

namespace Burgerber.Service.Interfeces.Notifications;

public interface ISmsSender
{
    public Task<bool> SendAsync(SmsMessange smsMessange);
}
