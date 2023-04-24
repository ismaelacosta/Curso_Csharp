using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IntroduccionAEFCore.Data;
using IntroduccionAEFCore.Data.Entities;
using IntroduccionAEFCore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ActorsController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            return await _context.Actors.OrderBy(a => a.BirthDate).ToListAsync(); // contatenar orden se agrega then by.
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string name)
        {
            return await _context.Actors.Where(a => a.Name == name).ToListAsync();
        }

        [HttpGet("{name}/v2")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetV2(string name)
        {
            return await _context.Actors.Where(a => a.Name.Contains(name)).ToListAsync();
        }

        [HttpGet("BirthDate/range")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(DateTime start, DateTime end)
        {
            return await _context.Actors.Where(d => d.BirthDate >= start && d.BirthDate <= end).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async   Task<ActionResult<Actor>> Get(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actor is null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        [HttpGet("IdyNombre")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> GetIdYName()
        {
            var actors = await _context.Actors.ProjectTo<ActorDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(actors);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ActorCreationDTO actorDTO)
        {

            if (actorDTO == null)
            {
                return NotFound();
            }

            var actor = _mapper.Map<Actor>(actorDTO);

            _context.Add(actor);

            await _context.SaveChangesAsync();

            return Ok(actorDTO);
        }
    }
}