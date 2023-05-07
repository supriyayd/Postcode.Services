using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LambdaFunction.Abstraction
{
    public interface IServiceConfiguration
    {
        public void Add(IConfiguration config, IServiceCollection serviceCollection);
    }
}
