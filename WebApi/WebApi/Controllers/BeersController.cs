using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Data.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeersController : ControllerBase
    {
        private readonly ILogger<BeersController> _logger;
        private readonly DataContext _context;

        public BeersController(ILogger<BeersController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetBeers")]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeers()
        {
            return await _context.Beers.ToListAsync();
        }

        [HttpGet("{id}/{name}", Name = "GetBeer")]
        public async Task<ActionResult<Beer>> GetProduct(int id, string name)
        {
            var product = await _context.Beers.FindAsync(id);
            Console.WriteLine(name);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Beer>> Post(Beer beer)
        {

            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetBeers", new { id = beer.Id }, beer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Beer beer)
        {
            if (id != beer.Id)
            {
                return BadRequest();
            }

            _context.Entry(beer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Beer>> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return beer;
        }

    }
}
