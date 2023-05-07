using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postcode.Services.Abstractions;
using Postcode.Services.Configurations;
using Postcode.Services.Implementation;

namespace Postcode.Services
{
    public static class PostcodeServiceBinder
    {
        public static IServiceCollection RegisterPostcodeService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IPostcodeService, PostcodeService>()
                    .Configure<PostcodeConfiguration>(configuration.GetSection("PostcodeURL"))
                    .Configure<MaxResultConfiguration>(configuration.GetSection("MaxResult"));


            return services;
        }
    }
}
