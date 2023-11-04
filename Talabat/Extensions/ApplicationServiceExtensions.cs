using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Interfaces;
using Talabat.Helpers;
using Talabat.Repository.Data;
using Talabat.Repository;
using Talabat.Errors;

namespace Talabat.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection Services)
        {


     
         
         


            Services.AddAutoMapper(typeof(MappingProfiles));


            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                                        .SelectMany(E => E.Value.Errors)
                                                        .Select(e => e.ErrorMessage).ToList();


                    var VaidationError = new ApiValidationError()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(VaidationError);

                };
            });




            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.AddScoped(typeof(IBasketRepository), typeof(BacketRepository));

          

            return Services;
        }

     
    }
}
