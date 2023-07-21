using ecommerce.Data;
using ecommerce.Domain;
using ecommerce.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAccountService, AccountService>();


var app = builder.Build();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
