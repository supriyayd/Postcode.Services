using LambdaFunction.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace LambdaFunction.Implementation
{
    class BaseHandlerConfiguration : IServiceConfiguration
    {
        public void Add(IConfiguration config, IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(logging =>
            {
                var loggerOptions = new LambdaLoggerOptions
                {
                    IncludeCategory = true,
                    IncludeLogLevel = true,
                    IncludeNewline = true,
                    IncludeEventId = true,
                    IncludeException = true
                };
                // Configure Lambda logging
                logging.AddLambdaLogger(loggerOptions);
                logging.SetMinimumLevel(LogLevel.Trace);
            });
            //
           //serviceCollection.AddDefaultAWSOptions(config.GetAWSOptions());
        }
    }
}
