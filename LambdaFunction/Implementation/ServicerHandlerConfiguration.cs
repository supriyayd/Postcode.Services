using LambdaFunction.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Postcode.Services;


namespace LambdaFunction.Implementation
{
    public class ServicerHandlerConfiguration : IServiceConfiguration
    {
        public void Add(IConfiguration config, IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterPostcodeService(config);
        }
    }
}
