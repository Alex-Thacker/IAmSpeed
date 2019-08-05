using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models.GameViewModels
{
    public class GameSegmentViewModel
    {
        public Game Game { get; set;  }
        public Segment Segment { get; set;  }
        public List<Segment> segmentsList { get; set; } = new List<Segment>(); 
    }
}
