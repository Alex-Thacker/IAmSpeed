using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{
    public class Following
    {
        public int Id { get; set;  }
        public string UserId { get; set;  }
        public ApplicationUser User { get; set;  }
        public string FollowingUserId { get; set;  }
        public ApplicationUser FollowingUser { get; set;  }
    }
}
