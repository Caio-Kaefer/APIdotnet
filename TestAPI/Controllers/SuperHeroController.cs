using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero
                {
                    Id = 1,
                    Name = "Spiderman",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
                }
            };


        [HttpGet]
        public ActionResult<IEnumerable<SuperHero>> Get()
        {
            return Ok(heroes);
        }  
        
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<SuperHero>> Get(int id)
        {
            var hero = heroes.Find(hero => hero.Id == id);
            if(hero == null)
            {
                return BadRequest("Hero not found.");
            }
            return Ok(hero);
        }

        [HttpPost]
        [Route("/add")]
        
        public ActionResult<IEnumerable<SuperHero>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpPut]
        public ActionResult<IEnumerable<SuperHero>> UpdateHero(SuperHero req)
        {

            var hero = heroes.Find(hero => hero.Id == req.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            
            hero.Name= req.Name;
            hero.FirstName= req.FirstName;
            hero.LastName= req.LastName;
            hero.Place= req.Place;
            return Ok(hero);
        }

        [HttpDelete("{id}")]
        public ActionResult<List<SuperHero>> DeleteHero(int id)
        {
            var hero = heroes.Find(hero => hero.Id == id);
            if (hero == null)
            {
                return NotFound("Hero not found.");
            }

            heroes.Remove(hero);
            return Ok(hero);
        }


    }
}
