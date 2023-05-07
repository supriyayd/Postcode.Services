using Postcode.Services.Abstractions;
using Postcode.Services.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Postcode.Services.Configurations;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Collections.Generic;

namespace Postcode.Services.Implementation
{
    public class PostcodeService : IPostcodeService
    {
        private HttpClient _httpClient;
        private readonly PostcodeConfiguration _postcodeConfiguration;
        private readonly MaxResultConfiguration _maxResultConfiguration;

        public PostcodeService(IOptions<PostcodeConfiguration> postcodeConfiguration, IOptions<MaxResultConfiguration> maxResultConfigutation)
        {
            _postcodeConfiguration = postcodeConfiguration.Value;
            _maxResultConfiguration=maxResultConfigutation.Value;
        }


        public async Task<List<string>> GetPartialPostcodeAsync(string postcode, int skip)
        {
            try
            {
                using (_httpClient = new HttpClient())
                {
                    using (var response = await _httpClient.GetAsync($"{_postcodeConfiguration.URL}/postcodes/{postcode}/autocomplete"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var result= JsonConvert.DeserializeObject<Partialpostalcode>(apiResponse);
                        return result.result.Skip((skip) * _maxResultConfiguration.Take)
                        .Take(_maxResultConfiguration.Take).ToList();
                    }
                }
            }
            catch (System.Exception )
            {

                return null;
            }


        }

        public async Task<PostalcodeDetailsResponse> GetPostcodeDetails(string postcode)
        {
            try
            {
                using (_httpClient = new HttpClient())
                {
                    using (var response = await _httpClient.GetAsync($"{_postcodeConfiguration.URL}/postcodes/{postcode}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var result= JsonConvert.DeserializeObject<LookupPostcode>(apiResponse);
                        return GetPostalcodeResponse(result);
                    }
                }
            }
            catch (System.Exception)
            {

                return null;
            }

        }


        private PostalcodeDetailsResponse GetPostalcodeResponse(LookupPostcode lookupPostcode)
        {
            string area = string.Empty;
            switch (lookupPostcode.result.Latitude)
            {
                case double x when x < 52.229466:
                        {
                        area = "South";
                        break;
                    }
                case double x when 52.229466 <=x && x < 53.27169:
                    {
                        area = "Midlands";
                        break;
                    }
                case double x when x >= 53.27169:
                    {
                        area = "North";
                        break;
                    }

                default:
                    break;
            }
            return new PostalcodeDetailsResponse(
                lookupPostcode.result.Country, 
                lookupPostcode.result.Region,
                lookupPostcode.result.Admin_district,
                lookupPostcode.result.Parliamentary_constituency,
                area
                );
  
        }
    }
}
