
using Ecommerce.Domain.Entities.Identity;
using Ecommerce.Domain.IRepositorys;
using Ecommerce.Errors;
using Ecommerce.Errors.middlewere_handel_exception;
using Ecommerce.Extenstions;
using Ecommerce.Helpers;
using Ecommerce.Repository.Date;
using Ecommerce.Repository.Date.Identity;
using Ecommerce.Repository.Repositorys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

namespace Ecommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb")));
            builder.Services.AddDbContext<IdentityDbContexts>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDb")));
            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var Connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(Connection);
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {

            }).AddEntityFrameworkStores<IdentityDbContexts>();

            builder.Services.AddApplicationServices();

            builder.Services.AddAuthentication(o =>
             {
                 o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(o =>
             {
                 o.SaveToken = true;
                 o.RequireHttpsMetadata = false;
                 o.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                     ValidateAudience = true,
                     ValidAudience = builder.Configuration["JWT:ValidAudience"],
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey =
                     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                 };

             });

            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowAllOrigins", o =>
                {
                    o.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();


                });
            });


            #region Swiger setting auth
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo", Version = "v1" });
            });
            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });

                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                });
            });

            #endregion

            var app = builder.Build();

            // auto update database by migration when application run
            #region apply migraiton during app run
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logerfactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await DbInitialze.Seed(context);
                var IdentityContext = services.GetRequiredService<IdentityDbContexts>();
                await IdentityContext.Database.MigrateAsync();
                var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
                await DbInitialzeIdentity.IdentitySeedAsync(userManger);

            }
            catch (Exception ex)
            {
                var loger = logerfactory.CreateLogger<Program>();
                loger.LogError(ex, " an error occurred during apply migration  ");
            }
            #endregion
            // Configure the HTTP request pipeline.

            app.UseMiddleware<ResponseException>(); // for response exception

            if (app.Environment.IsDevelopment())
            {
                app.AddSwaggerServicesMiddleware();
            }
            app.UseStatusCodePagesWithRedirects("error/{0}");
            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
