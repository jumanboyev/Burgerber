namespace Burgerber.Domain.Exseptions.Categories
{
    public class CategoryNotFoundExseption : NotFoundExseption
    {
        public CategoryNotFoundExseption()
        {
            this.TitleMessage = "Category Not Found";
        }
    }
}
