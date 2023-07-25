using System.Net;

namespace Burgerber.Domain.Exseptions;

public class TooManyRequest : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.TooManyRequests;

    public string TitleMessage { get; protected set; } = string.Empty;
}
