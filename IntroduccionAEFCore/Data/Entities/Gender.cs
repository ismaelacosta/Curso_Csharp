using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IntroduccionAEFCore.Data.Entities
{
    public class Gender
    {

        public int Id { get; set; }

        [StringLength(maximumLength:150,ErrorMessage ="String inserted is to much")]
        public string Name { get; set;} 

        public HashSet<Movie>? Movies { get; set; }
    }
}