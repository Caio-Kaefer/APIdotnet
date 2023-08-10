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


        public DataContext Context { get; }

        public SuperHeroController(DataContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<SuperHero>> Get()
        {
            return Ok(await Context.Superheroes.ToListAsync());
        }  
        
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SuperHero>>> Get(int id)
        {
            var hero = await Context.Superheroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero not found.");
            }
            return Ok(hero);
        }

        [HttpPost]
        [Route("/add")]
        
        public async Task<ActionResult<SuperHero>> AddHero(SuperHero hero)
        {
            Context.Superheroes.Add(hero);
            await Context.SaveChangesAsync();
            return Ok(await Context.Superheroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero req)
        {

            var hero = await Context.Superheroes.FindAsync(req.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }

            hero.Name= req.Name;
            hero.FirstName= req.FirstName;
            hero.LastName= req.LastName;
            hero.Place= req.Place;
            await Context.SaveChangesAsync();
            return Ok(await Context.Superheroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await Context.Superheroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            Context.Superheroes.Remove(hero);
            await Context.SaveChangesAsync();
            return Ok(Context.Superheroes.ToListAsync());
        }


    }
}
