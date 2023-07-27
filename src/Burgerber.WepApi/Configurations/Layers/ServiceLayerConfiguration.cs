using Burgerber.Service.Interfeces.Auth;
using Burgerber.Service.Interfeces.Categories;
using Burgerber.Service.Interfeces.Common;
using Burgerber.Service.Interfeces.Notifications;
using Burgerber.Service.Interfeces.Products;
using Burgerber.Service.Services.Auth;
using Burgerber.Service.Services.Categories;
using Burgerber.Service.Services.Common;
using Burgerber.Service.Services.Notifications;
using Burgerber.Service.Services.Products;

namespace Burgerber.WepApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddSingleton<ISmsSender, SmsSender>();
            builder.Services.AddScoped<IProductService, ProductService > ();
            builder.Services.AddScoped<IFileService, FileService>();

        }
    }
}
