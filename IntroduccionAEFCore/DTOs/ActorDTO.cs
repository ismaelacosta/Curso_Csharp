using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IntroduccionAEFCore.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }


        [StringLength(maximumLength: 150, ErrorMessage = "String inserted is to much")]
        public string Name { get; set; }
    }
}