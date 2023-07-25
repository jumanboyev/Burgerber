using System.Net;

namespace Burgerber.Domain.Exseptions;

public class BadRequestExseption : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;

    public string TitleMessage { get; protected set; } = string.Empty;
}
