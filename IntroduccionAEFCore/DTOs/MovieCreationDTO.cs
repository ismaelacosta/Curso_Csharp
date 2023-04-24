using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroduccionAEFCore.DTOs
{
    public class MovieCreationDTO
    {
        public string Title { get; set; }
        public bool IsInCinema { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<int> Genders { get; set; } = new List<int>(); // para enlazar una pelicula con un genero solamente necesito un id
        public List<MovieActorCreationDTO> MovieActors { get; set; } = new List<MovieActorCreationDTO>();
    }
}