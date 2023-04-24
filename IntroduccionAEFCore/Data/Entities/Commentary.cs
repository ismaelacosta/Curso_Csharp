using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroduccionAEFCore.Data.Entities
{
    public class Commentary
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public bool IsRecommended { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}