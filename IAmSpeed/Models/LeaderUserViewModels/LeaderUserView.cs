using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models.LeaderUserViewModels
{
    public class LeaderUserView
    {
        public LeaderBase leaderBase { get; set;  }
        public SinglePlayerBase singlePlayerBase { get; set;  }
        public List<SinglePlayerBase> singlePlayerList { get; set; } = new List<SinglePlayerBase>();
    }
}
