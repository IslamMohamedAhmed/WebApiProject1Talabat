using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.API.Errors;
using Talabat.API.Profiles;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services) {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(w => w.Value.Errors.Count > 0).SelectMany(s => s.Value.Errors).Select(s => s.ErrorMessage).ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            return Services;
        }
        
    }
}
