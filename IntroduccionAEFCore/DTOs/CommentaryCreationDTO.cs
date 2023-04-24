using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroduccionAEFCore.DTOs
{
    public class CommentaryCreationDTO 
    {
        public string? Content { get; set; }    
        public bool IsRecommended { get; set; }
    }
}