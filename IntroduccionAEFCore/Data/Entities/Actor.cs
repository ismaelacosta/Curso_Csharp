using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroduccionAEFCore.Data.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "String inserted is to much")]
        public string Name { get; set; }

        public decimal MoneySaved { get; set; }

        public DateTime BirthDate { get; set; }

        public List<MovieActor>? MovieActors { get; set; } = new List<MovieActor>();

    }
}