using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using LambdaFunction.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Postcode.Services.Abstractions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaFunction
{
    public class PostcodeLambda : BaseLambdaHandler
    {
        ILambdaLogger _logger;
        IPostcodeService _postcodeService;
        const string Postcode = "postcode";

        public PostcodeLambda()
        {
           ConfigureServices(new ServicerHandlerConfiguration());
        }

        /// <summary>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="serviceProvider"></param>
        public PostcodeLambda(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="lambdaContext"></param>
        /// <returns></returns>
        public async Task<APIGatewayHttpApiV2ProxyResponse> Handle(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext lambdaContext)
        {
            try
            {
                _logger = lambdaContext.Logger;

                _logger.LogLine($"Request: {request.RawQueryString}.");
                _postcodeService = ServiceProvider.GetService<IPostcodeService>();
                if (string.IsNullOrEmpty(request.QueryStringParameters[Postcode]) || string.IsNullOrEmpty(request.QueryStringParameters[Postcode]))
                {
                    _logger.LogLine($"Missing required query parameters.");
                    return new APIGatewayHttpApiV2ProxyResponse
                    {
                        StatusCode = 400,

                    };
                }
                var postcode = request.QueryStringParameters[Postcode];
                var postcodeDetails = await _postcodeService.GetPostcodeDetails(postcode);
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = 200,
                    Body = JsonSerializer.Serialize(postcodeDetails),

                };

            }
            catch (Exception ex)
            {

                _logger.LogLine($"Failed to get pending requests for products. exception occured {ex.Message}");
                return new APIGatewayHttpApiV2ProxyResponse
                {
                    StatusCode = 500,

                };
            }
        }  
    }
}
