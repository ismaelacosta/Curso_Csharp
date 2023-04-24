using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IntroduccionAEFCore.Data;
using IntroduccionAEFCore.Data.Entities;
using IntroduccionAEFCore.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MoviesController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {

            var movie = await _context.Movies
            .Include(g => g.Genders)
            .Include(p => p.Commentaries)
            .Include(a => a.MovieActors.OrderBy(pa => pa.Order))
                .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(movie is null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

         [HttpGet("select/{id:int}")]
        public async Task<ActionResult<Movie>> GetSelect(int id)
        {

            var movie = await _context.Movies
                .Select( pel => new {
                    pel.Id,
                    pel.Title,
                    Genders = pel.Genders.Select(g => g.Name).ToList(),
                    Actors = pel.MovieActors.OrderBy(pa => pa.Order).Select( pa => new {
                        Id = pa.ActorId,
                        pa.Actor.Name,
                        pa.Character
                    }),
                    CommnetariesQuantity = pel.Commentaries.Count()
                }).FirstOrDefaultAsync();

            if(movie is null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MovieCreationDTO movieDTO)
        {
            if (movieDTO == null)
            {
                return NotFound();
            }

            var movie = _mapper.Map<Movie>(movieDTO);

            if (movie.Genders is not null)
            {
                foreach (var gender in movie.Genders)
                {
                    _context.Entry(gender).State = EntityState.Unchanged; //Entry le da seguimiento a los objetos que ya existen no tiene que crear uno nuevo
                }
            }

            if (movie.MovieActors is not null)
            {
                for (int i = 0; i < movie.MovieActors.Count; i++)
                {
                     movie.MovieActors[i].Order = i + 1;
                }
            }

            _context.Add(movie);
            await _context.SaveChangesAsync();

            return Ok(movieDTO);
        }


         [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var rowsChanged = await _context.Movies.Where(g => g.Id == id).ExecuteDeleteAsync();

            if(rowsChanged == 0)
            {
                return NotFound();
            }

            return Ok(rowsChanged);
        }
    }
}