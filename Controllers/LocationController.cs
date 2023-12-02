using Microsoft.AspNetCore.Mvc;
using RnM.Api.DB;
using RnM.Api.Models;

namespace RnM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
