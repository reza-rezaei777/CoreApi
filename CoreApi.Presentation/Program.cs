using CoreApi.DataLayer;
using CoreApi.Services.Services;
using CoreApi.WebFramework.Middlewares;
using Data.Repositories;
using ElmahCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.
//Add a sql server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

//register service
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddJwtAuthentication();

builder.Services.AddControllers();
builder.Services.AddElmah(options =>
{
    options.Path = "/elmah-errors";
    options.ConnectionString = builder.Configuration.GetConnectionString("SqlServer");
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseElmah();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
