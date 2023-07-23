using Burgerber.DataAccess.Interfaces.Categories;
using Burgerber.DataAccess.Interfaces.Clients;
using Burgerber.DataAccess.Repositories.Categories;
using Burgerber.DataAccess.Repositories.Clients;
using Burgerber.Service.Interfeces.Auth;
using Burgerber.Service.Interfeces.Categories;
using Burgerber.Service.Interfeces.Notifications;
using Burgerber.Service.Services.Auth;
using Burgerber.Service.Services.Categories;
using Burgerber.Service.Services.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

//->
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddSingleton<ISmsSender, SmsSender > ();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
