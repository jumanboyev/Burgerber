using System.ComponentModel.DataAnnotations;

namespace Burgerber.Domain.Entities.Clients;

public class Client:Human
{
    [MaxLength(13)]
    public string PhoneNumber { get; set; } = string.Empty;

    public string PhoneNumberConfirmed { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;

}
