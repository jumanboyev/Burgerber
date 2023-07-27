using Burgerber.DataAccess.Interfaces.Categories;
using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.DataAccess.Interfaces.Discounts;
using Burgerber.DataAccess.Interfaces.Products;
using Burgerber.DataAccess.Repositories.Categories;
using Burgerber.DataAccess.Repositories.Clients;
using Burgerber.DataAccess.Repositories.Discounts;
using Burgerber.DataAccess.Repositories.Products;

namespace Burgerber.WepApi.Configurations.Layers
{
    public static class DataAccessConfuguration
    {
        public static void ConfidureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
           

        }
    }
}
