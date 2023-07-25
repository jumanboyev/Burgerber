namespace Burgerber.Domain.Exseptions.Admin;

public class AdminNotFoundExseption : NotFoundExseption
{
    public AdminNotFoundExseption()
    {
        this.TitleMessage = "Admin not found";
    }
}
