using System.Net;

namespace Burgerber.Domain.Exseptions;

public class ExpiredExseption : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.Gone;

    public string TitleMessage { get; protected set; } = string.Empty;
}
