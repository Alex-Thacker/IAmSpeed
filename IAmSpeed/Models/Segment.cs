using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{
    public class Segment
    {
        public int Id { get; set;  }
        public int? OrginId { get; set;  }
        public string Name { get; set; }
        public string Description { get; set;  }
        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }
        public string Notes { get; set;  }
        [Display(Name = "Personal Best Time")]
        public string PBTime { get; set; }
        
        public string RNG { get; set; }
        public string Category { get; set; }
        public string UserId { get; set;  }
        public string GameIdFromAPI { get; set;  }
        public ApplicationUser User { get; set;  }
        public int GameId { get; set;  }
        public Game Game { get; set;  }

    }
}
