namespace Burgerber.Domain.Exseptions.Delivers;

public class DeliverNotFoundExseption : NotFoundExseption
{
    public DeliverNotFoundExseption()
    {
        this.TitleMessage = "Deliver not found ";
    }
}
