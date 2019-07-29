using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{
    public class Game
    {
        public int Id { get; set;  }
        public int GameIdFromAPI { get; set;  }
        public string Name { get; set;  }
        public string Picture { get; set;  }
        public DateTime ReleaseDate { get; set;  }
        public string UserId { get; set;  }
        public ApplicationUser User { get; set;  }
    }
}
