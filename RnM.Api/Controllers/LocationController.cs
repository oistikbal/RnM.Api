using Microsoft.AspNetCore.Mvc;
using RnM.Api.Auth;
using RnM.Api.DB;
using RnM.Api.Models;

namespace RnM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class LocationController : ControllerBase
    {
        private RnMContext _context;
        public LocationController(RnMContext rnmContext)
        {
            _context = rnmContext;
        }

        [HttpGet("{id:int}")]
        public Location Get(int id)
        {
            return _context.Location.Find(id);
        }


        [HttpGet]
        public IQueryable<Location> GetLocations([FromQuery(Name = "page")] int page)
        {
            const int paginationCount = 5;
            return _context.Location.OrderBy(e => e.Id).Skip(paginationCount * page).Take(5);
        }
    }
}
