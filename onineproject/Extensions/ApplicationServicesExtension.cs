using Microsoft.AspNetCore.Mvc;
using onine.core.Repositories;
using onine.Repository;
using onineproject.Errors;
using onineproject.Helpers;

namespace onineproject.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<ProductpictureurlResolver>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));

            //builder.Services.AddAutoMapper(typeof(MappingProfiles));
            Services.Configure<ApiBehaviorOptions>(options =>
             {
                 options.InvalidModelStateResponseFactory = actionContext =>
                 {
                     var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                      .SelectMany(p => p.Value.Errors)
                      .Select(E => E.ErrorMessage).ToList();
                     var validationErrorResponse = new ApiValidationErrorResponse
                     {
                         Errors = errors
                     };
                     return new BadRequestObjectResult(validationErrorResponse);


                 };
             });
            return Services;
        }




        }
}
