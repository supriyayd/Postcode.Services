using LambdaFunction.Abstraction;
using LambdaFunction.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace LambdaFunction.Implementation
{
    public class BaseLambdaHandler
    {
        private IConfiguration Config { get; set; }
        protected IServiceCollection Services { get; set; }
        protected IServiceProvider ServiceProvider { get; private set; }

        public BaseLambdaHandler()
        {
            BuildConfigAndServices();
            AddServices(new BaseHandlerConfiguration());
        }

        /// <summary>
        /// This constructor is used for testing.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public BaseLambdaHandler(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        private void BuildConfigAndServices()
        {
            var env = GetEnvironment();
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Services = new ServiceCollection();
            Services.AddOptions();
        }

        private string GetEnvironment() =>
         Environment.GetEnvironmentVariable(EnvironmentVariables.AspnetCoreEnvironment) ?? "Development";

        protected void AddServices(IServiceConfiguration serviceConfig)
        {
            serviceConfig.Add(Config, Services);
        }

        protected void ConfigureServices()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }

        protected void ConfigureServices(IServiceConfiguration serviceConfig)
        {
            serviceConfig.Add(Config, Services);
            ConfigureServices();
        }
    }
}
