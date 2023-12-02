using Microsoft.AspNetCore.Mvc;
using RnM.Api.Auth;
using RnM.Api.DB;
using RnM.Api.Models;

namespace RnM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class EpisodeController : ControllerBase
    {
        private RnMContext _context;
        public EpisodeController(RnMContext rnmContext)
        {
            _context = rnmContext;
        }

        [HttpGet("{id:int}")]
        public Episode Get(int id)
        {
            return _context.Episodes.Find(id);
        }

        [HttpGet]
        public IQueryable<Episode> GetPages([FromQuery(Name = "page")] int page)
        {
            const int paginationCount = 5;
            return _context.Episodes.OrderBy(e => e.Id).Skip(paginationCount * page).Take(5);
        }
    }
}
