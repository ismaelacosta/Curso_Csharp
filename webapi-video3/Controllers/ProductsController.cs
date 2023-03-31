using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi_video3.Data;
using webapi_video3.Models;

namespace webapi_video3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        private readonly DataContext _context;

        public ProductsController(ILogger<ProductsController> logger, DataContext context)
        {
            _logger = logger;
            _context =  context;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){
            return await _context.Products.ToListAsync();
        }


        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null){
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product){

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetProduct", new {id = product.Id}, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Product product){
            if (id != product.Id){
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null){
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }


    }
}