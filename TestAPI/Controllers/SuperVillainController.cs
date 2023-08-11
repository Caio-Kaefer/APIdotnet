using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperVillainController : ControllerBase
    {
        public DataContext Context { get; }

        public SuperVillainController(DataContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperVillain>>> Get()
        {
            return Ok(await Context.SuperVillains.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperVillain>> Get(int id)
        {
            var vil = await Context.SuperVillains.FindAsync(id);
            if (vil == null)
            {
                return BadRequest("Villain not found.");
            }
            return Ok(vil);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<SuperVillain>>> AddHero(SuperVillain vil)
        {
            Context.SuperVillains.Add(vil);
            await Context.SaveChangesAsync();
            return Ok(await Context.SuperVillains.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<SuperVillain>>> UpdateHero(SuperVillain req)
        {
            var vil = await Context.SuperVillains.FindAsync(req.Id);
            if (vil == null)
            {
                return BadRequest("Villain not found.");
            }

            vil.Name = req.Name;
            vil.FirstName = req.FirstName;
            vil.LastName = req.LastName;
            vil.Place = req.Place;
            await Context.SaveChangesAsync();
            return Ok(await Context.SuperVillains.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<SuperVillain>>> DeleteHero(int id)
        {
            var vil = await Context.SuperVillains.FindAsync(id);
            if (vil == null)
            {
                return BadRequest("Villain not found.");
            }
            Context.SuperVillains.Remove(vil);
            await Context.SaveChangesAsync();
            return Ok(await Context.SuperVillains.ToListAsync());
        }
    }
}
