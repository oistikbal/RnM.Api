using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Mvc;
using RnM.Api.Auth;
using RnM.Api.DB;
using RnM.Api.Models;
using System;
using System.Web.Http.Routing;
using static Bogus.DataSets.Name;
using static System.Net.Mime.MediaTypeNames;

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


        // Couldn't find any other way to serialize data, i leave it as it is
        [HttpGet]
        public IQueryable<CharacterDto> GetPages([FromQuery(Name = "page")] int page)
        {
            const int paginationCount = 5;
            var characters = _context.Characters.OrderBy(e => e.Id).Skip(Math.Clamp(paginationCount * page -1, 0, Int32.MaxValue)).Take(5);

            var charactersDto = from c in characters
                        select new CharacterDto()
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Gender = Enum.GetName(c.Gender),
                            Image = c.Image,
                            Origin = c.Origin,
                            Species = c.Species,
                            Url = c.Url,
                            Location = new List<string> { c.Location.Name, Url.Action("Get", "Location", new {c.Location.Id },Request.Scheme), },
                            Created = c.Created,
                            Type = c.Type,
                            Status = Enum.GetName(c.Status),
                            Episodes = null
                        };

            return charactersDto;
        }

        //Memory leak
        [NonAction]
        private CharacterDto Convert(Character character)
        {
            var characterDto = new CharacterDto();

            characterDto.Id = character.Id;
            characterDto.Name = character.Name;
            characterDto.Gender = Enum.GetName(character.Gender);
            characterDto.Image = character.Image;
            characterDto.Origin = character.Origin;
            characterDto.Species = character.Species;
            characterDto.Url = character.Url;
            characterDto.Location = new List<string> { character.Location.Name, Url.Action("Get", "Location", new { character.Location.Id }, Request.Scheme)};
            characterDto.Created = character.Created;
            characterDto.Type = character.Type;
            characterDto.Status = Enum.GetName(character.Status);
            characterDto.Episodes = null;

            return characterDto;
        }
        
    }
}
