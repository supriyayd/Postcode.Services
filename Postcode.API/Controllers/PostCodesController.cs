using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postcode.Services.Abstractions;
using Postcode.Services.Implementation;
using System.Linq;
using System.Threading.Tasks;

namespace Postcode.API.Controllers
{

    [Route("v1/[controller]")]
    [ApiController]
    public class PostCodesController : ControllerBase
    {
        IPostcodeService _postcodeService;
        public PostCodesController(IPostcodeService postcodeService)
        {
            _postcodeService=postcodeService;
        }

        [HttpGet]
        [Route("GetPartialPostcode")]
        public async Task<ActionResult> GetPartialPostcode(string postcode, int skip=0)
        {
            try
            {
                var respose = await _postcodeService.GetPartialPostcodeAsync(postcode, skip);

                if (respose!=null && respose.Any())
                {
                    return Ok(respose);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (System.Exception)
            {

                return NotFound();
            }
           
        }


        [HttpGet]
        [Route("GetPostcodeDetails")]
        public async Task<ActionResult> GetPostcodeDetails(string postcode)
        {
            try
            {
                var respose = await _postcodeService.GetPostcodeDetails(postcode);

                if (respose!=null)
                {
                    return Ok(respose);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (System.Exception)
            {

                return NotFound();
            }

        }
    }
}
