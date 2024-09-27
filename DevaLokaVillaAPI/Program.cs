
using DevaLokaVillaAPI.Data;
using DevaLokaVillaAPI.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DevaLokaVillaAPI.Models;
using DevaLokaVillaAPI;
using System.Text;
using DevaLokaVillaAPI.Repository.IRepository;
using DevaLokaVillaAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Custom serial Logging function
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().
//    WriteTo.File("log/villaLogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(option => {

    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddResponseCaching();
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(MappingConfig));
var apiVersioningBuilder = builder.Services.AddApiVersioning( options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1,0);
    options.ReportApiVersions = true;
});

apiVersioningBuilder.AddApiExplorer (options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add("Default30",
        new CacheProfile()
        {
               Duration = 30
        });

   // option.ReturnHttpNotAcceptable=true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.0",
        Title = "Deva Loka Villa V1",
        Description = "API to manage Villa",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name =  "Dot Net API",
            Url = new Uri("https://dotnetexample.com")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://dotnetexample.com/license")
        }
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2.0",
        Title = "Deva Loka Villa V2",
        Description = "API to manage Villa",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Dot Net API",
            Url = new Uri("https://dotnetexample.com")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://dotnetexample.com/license")
        }
    });




});

//builder.Services.AddSingleton<ILogging, Logging>();

//builder.Services.AddSingleton<ILogging, LoggingV2>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json","DevaLokaVillaV1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "DevaLokaVillaV2");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Note add before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
