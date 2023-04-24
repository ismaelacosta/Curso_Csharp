using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IntroduccionAEFCore.DTOs
{
    public class GenderCreationDTO
    {
        [StringLength(maximumLength:150)]
        public string Name { get; set; }
    }
}