﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{
    public class Section
    {
        public int Id { get; set;  }
        public string Name { get; set; }
        public string Description { get; set;  }
        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }
        public string Notes { get; set;  }
        public DateTime PBTime { get; set; }
        public string RNG { get; set; }
        public string Category { get; set; }
        public string UserId { get; set;  }
        public ApplicationUser User { get; set;  }
        public int GameId { get; set;  }
        public Game Game { get; set;  }

    }
}
