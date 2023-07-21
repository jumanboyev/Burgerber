using System.Net;

namespace Burgerber.Domain.Exseptions;

public class AlreadyExistsExseption:Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;

    public string TitleMessage { get; protected set; } = string.Empty;
}
