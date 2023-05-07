
namespace Postcode.Services.Models
{
    public class PostalcodeDetailsResponse
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string Admin_District { get; set; }
        public string Parliamentary_Constituency { get; set; }
        public string Area { get; set; }

        public PostalcodeDetailsResponse(string country, string region, string adminDistrict, string parliamentaryConstituency, string area) =>
            (Country, Region, Admin_District, Parliamentary_Constituency, Area) =
            (country, region, adminDistrict, parliamentaryConstituency, area);
       
    }
}
