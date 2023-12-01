using Microsoft.AspNetCore.Mvc;
using RnM.Api.Models;

namespace RnM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {

        [HttpGet("{id:int}")]
        public Character Get(int id)
        {
            return new Character();
        }


    }
}
