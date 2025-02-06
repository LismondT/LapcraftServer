using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using LapcraftServer.Domain.Interfaces;
using LapcraftServer.Application.Interfaces.Auth;
using LapcraftServer.Application.Services;
using LapcraftServer.Infastructure.Services.Auth;
using LapcraftServer.Persistens.Repositories;
using LapcraftServer.Persistens;
using Microsoft.EntityFrameworkCore;

namespace LapcraftServer.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        
        ConfigureServices(builder);

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        //Auth options
        string jwtKeyValue = builder.Configuration.GetSection("Jwt")["Key"]
            ?? throw new Exception("Key value for jwt token was not founded");

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKeyValue)
                    ),
                };

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["syndetheite"];
                        return Task.CompletedTask;
                    }
                };
            });

        builder.Services.AddControllers();
     
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //BD
        builder.Services.AddDbContext<LapcraftDbContext>(
            options => {
                options.UseSqlite(builder.Configuration.GetConnectionString(nameof(LapcraftDbContext)));
            });

        //DI

        //Repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        //Service Dependencies
        //--Auth
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

        //Services
        builder.Services.AddScoped<IAuthService, AuthService>();
    }
}
