using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroduccionAEFCore.Data.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "String inserted is to much")]
        public string Title { get; set; }

        public bool? IsInCinema { get; set; }

        public DateTime ReleaseDate { get; set; }

        public HashSet<Commentary>? Commentaries { get; set; } // es mas rapido trabar con colecciones, pero no te devuelve datos ordenados

        public HashSet<Gender>? Genders { get; set; }

        public List<MovieActor>? MovieActors { get; set; } = new List<MovieActor>();
    }
}