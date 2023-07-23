namespace Burgerber.Service.Dtos.Notifications;

public class SmsMessange
{
    public string Recipent { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}
