using Bogus;
using Microsoft.AspNetCore.Mvc;
using RnM.Api.Auth;
using RnM.Api.DB;
using RnM.Api.Models;

namespace RnM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class CharacterController : ControllerBase
    {
        private RnMContext _context;
        public CharacterController(RnMContext rnmContext) 
        {
            _context = rnmContext;
        }

        [HttpGet("{id:int}")]
        public Character Get(int id)
        {
            return _context.Characters.Find(id);
        }

        
        [HttpGet]
        public IQueryable<Character> Get()
        {
            return GetCharacters(0);
        }

        [HttpGet("{page:int}")]
        public IQueryable<Character> GetCharacters(int page)
        {
            const int paginationCount = 5;
            return _context.Characters.OrderBy(e => e.Id).Skip(paginationCount * page).Take(5);
        }
        
    }
}
