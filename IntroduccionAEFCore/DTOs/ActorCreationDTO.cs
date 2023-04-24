using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroduccionAEFCore.DTOs
{
    public class ActorCreationDTO
    {
        public string Name { get; set; }
        public decimal MoneySaved { get; set; }
        public DateTime BirthDate { get; set; } 

    }
}