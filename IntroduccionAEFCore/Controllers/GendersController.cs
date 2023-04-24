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
    public class GendersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GendersController(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> Get()
        {
            return await _context.Genders.ToListAsync();
        }

        [HttpPost("List")]
        public async Task<IActionResult> Post(GenderCreationDTO[] genderDTO) // permite regrsar varias cosas el action result como un json
        {
            if(genderDTO == null){
                return NotFound();
            }

            var gender = _mapper.Map<Gender[]>(genderDTO);

            await _context.AddRangeAsync(gender);
            await _context.SaveChangesAsync();
            return Ok(genderDTO);
        } 

        [HttpPost]  //Dto permite crear clases para enviar, esconde y permite controlar que mostramos de nuestras entidades
        public async Task<IActionResult> Post(GenderCreationDTO genderDTO) //async es buena practica para realizar operation I/O - sistema se comunica con otro sistema app - Database
        {
            if(genderDTO == null){
                return NotFound();
            }

            var isReal = await _context.Genders.AnyAsync(g => g.Name == genderDTO.Name);

            if(isReal){
                return BadRequest("The gender name put exists in the database");
            }

            var gender = _mapper.Map<Gender>(genderDTO);
            _context.Genders.Add(gender);

            await _context.SaveChangesAsync();

            return Ok(gender);

        }

        [HttpPut("{id:int}/nombre2")]
        public async Task<ActionResult> Put(int id)
        {
            var gender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == id);

            if(gender is null)
            {
                return NotFound();
            }

            gender.Name = "Ismael";

            await _context.SaveChangesAsync();

            return Ok(gender);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GenderCreationDTO genderDTO)
        {
            var gender = _mapper.Map<Gender>(genderDTO);

            gender.Id = id;
            _context.Update(gender);
            await _context.SaveChangesAsync();
            return Ok(gender);

        }

        [HttpDelete("{id:int}/moderna")]
        public async Task<ActionResult> Delete(int id)
        {
            var rowsChanged = await _context.Genders.Where(g => g.Id == id).ExecuteDeleteAsync();

            if(rowsChanged == 0)
            {
                return NotFound();
            }

            return Ok(rowsChanged);
        }

         [HttpDelete("{id:int}/anterior")]
        public async Task<ActionResult> DeleteAnterior(int id)
        {
            var gender = await _context.Genders.FirstOrDefaultAsync(g => g.Id == id);

            if(gender is null)
            {
                return NotFound();
            }

            _context.Remove(gender);
        await _context.SaveChangesAsync();

            return Ok(gender);
        }



        
    }
}