using Postcode.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Postcode.Services.Abstractions
{
    public interface IPostcodeService
    {
        Task<List<string>> GetPartialPostcodeAsync(string postcode, int skip);

        Task<PostalcodeDetailsResponse> GetPostcodeDetails(string postcode);
    }

    
}
