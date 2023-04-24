using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroduccionAEFCore.Data.Entities;
using IntroduccionAEFCore.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using IntroduccionAEFCore.DTOs;


namespace IntroduccionAEFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentariesController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CommentariesController(DataContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(int MovieId, CommentaryCreationDTO commentaryDTO)
        {
            var commentary = _mapper.Map<Commentary>(commentaryDTO);
            commentary.MovieId = MovieId;
            _context.Add(commentary);
            await _context.SaveChangesAsync();
            return Ok(commentary);
        }
    }
}