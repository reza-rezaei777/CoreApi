using CoreApi.DataLayer;
using CoreApi.Domin;
using CoreApi.Services.Services;
using CoreApi.WebFramework.Configuration;
using CoreApi.WebFramework.Middlewares;
using Data.Repositories;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Host.UseNLog();
var siteSettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();

builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));
// Add services to the container.
//Add a sql server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddCustomIdentity(siteSettings.IdentitySettings);
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

//register service
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddJwtAuthentication(siteSettings.JWTSettings);

builder.Services.AddControllers();
builder.Services.AddElmah(options =>
{
    options.Path= siteSettings.ElmahPath;
    options.ConnectionString = builder.Configuration.GetConnectionString("SqlServer");
    //options.OnPermissionCheck = httpcontext =>
    //{
    //    return httpcontext.User.Identity.IsAuthenticated;
     
    //};
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();