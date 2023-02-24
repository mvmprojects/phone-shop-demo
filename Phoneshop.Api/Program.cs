using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string SpecificOrigins = "_allowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();//.AddJsonOptions(o => o.JsonSerializerOptions
                                  //.ReferenceHandler = ReferenceHandler.Preserve);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: SpecificOrigins,
      policyBuilder =>
      {
          policyBuilder//.WithOrigins("http://localhost:4200")
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      });
});

ConfigurationManager config = builder.Configuration;
// connection string should stay in appsettings.json 
// for security reasons, do not use .EnableSensitiveDataLogging in a real production application.
builder.Services.AddDbContext<DataContext>(options => options
    .UseSqlServer(config.GetConnectionString("DatabaseConnection"))
    .EnableSensitiveDataLogging());

// add microsoft identity - see also: IdentityDbContext
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);
// and authentication - needs microsoft.aspnetcore.authentication.jwtbearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = true;
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, //true,
            ValidateAudience = false, //true,
            //ValidIssuer = config["Authentication:ValidIssuer"],
            //ValidAudience = config["Authentication:ValidAudience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(config["Authentication:SecretForKey"])),
            ValidateLifetime = true,
            ClockSkew = System.TimeSpan.Zero
        };
    });

builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSingleton<ICaching, Caching>();
builder.Services.AddScoped<IBasicLogger, DbLogger>();

var app = builder.Build();

// add config values for the caching class
var cache = app.Services.GetService<ICaching>();
if (cache != null)
{
    cache.SlidingExpSeconds = int.Parse(config
        .GetSection("ExpirationPolicies:SlidingExpirationSeconds").Value);
    cache.AbsoluteExpSeconds = int.Parse(config
        .GetSection("ExpirationPolicies:AbsoluteExpirationSeconds").Value);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Added this line, and the else{} below, per old tutorial example
    // to avoid showing error details outside of development scenarios.
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Unexpected server fault. Try again later.");
        });
    });
}

app.UseHttpsRedirection();

app.UseCors(SpecificOrigins); // see above

app.UseAuthentication(); // added with microsoft identity (must go before Authorization)

app.UseAuthorization();

app.MapControllers();

app.Run();
