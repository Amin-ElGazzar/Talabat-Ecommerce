using Ecommerce.Domain.IRepositorys;
using Ecommerce.Domain.IServices;
using Ecommerce.Errors;
using Ecommerce.Helpers;
using Ecommerce.Repository.Repositorys;
using Ecommerce.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ecommerce.Extenstions
{
    public static class ApplicationServicesExtension
    {
        
        public static IServiceCollection AddApplicationServices( this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(ITokenServices),typeof(TokenServices));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
            services.AddAutoMapper(typeof(MappingProfiles));

            //validation errors handle
            services.Configure<ApiBehaviorOptions>(O =>
            O.InvalidModelStateResponseFactory = (ActionContext) =>
            {
                var errors = ActionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                                                     .SelectMany(o => o.Value.Errors)
                                                     .Select(e => e.ErrorMessage)
                                                     .ToArray();


                var apiValidationErrors = new ApiResponseValidationErrors()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(apiValidationErrors);
            });

           
            return services;
        }
    }
}
