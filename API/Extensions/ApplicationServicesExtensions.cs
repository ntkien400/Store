using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.IRepository;
using Infrastructure.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericReposiory<>));
            services.Configure<ApiBehaviorOptions>(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count >0)
                        .SelectMany( e => e.Value.Errors)
                        .Select(e => e.ErrorMessage).ToArray();

                        var errorResponse = new ApiValidationResponse
                        {
                            Errors = errors
                        };

                        return new BadRequestObjectResult(errorResponse);
                    };
                });
            return services;
        }
        
    }
}