﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postcode.Services.Abstractions;
using Postcode.Services.Implementation;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaAPI.Controllers;


    [Route("api/[controller]")]

    public class PostCodesController : ControllerBase
    {
        IPostcodeService _postcodeService;
        public PostCodesController(IPostcodeService postcodeService)
        {
            _postcodeService=postcodeService;
        }

        [HttpGet()]
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


        [HttpGet("{postcode}")]
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

