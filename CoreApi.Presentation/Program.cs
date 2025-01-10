using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreApi.Domin;
using CoreApi.WebFramework.Configuration;
using CoreApi.WebFramework.Middlewares;
using Data.Repositories;
using ElmahCore.Mvc;
using NLog.Web;


var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.UseNLog();
var siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
// Add services to the container:

//Add a sql server
builder.Services.AddDbContext(builder.Configuration);
//Add Identity
builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);
//Add MVC
builder.Services.AddMinimalMvc();
//register service
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//AddElmah 
builder.Services.AddElmah(builder.Configuration, siteSettings);
//Add Jwt
builder.Services.AddJwtAuthentication(siteSettings.JWTSettings);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Replace the default DI container with Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Configure Autofac container
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Centralized Autofac service registration
    containerBuilder.AddServices();
});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<CustomExceptionHandlerMiddleware>();
app.UseHsts(app.Environment);
app.UseElmah();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();