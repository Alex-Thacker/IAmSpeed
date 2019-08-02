using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{

    public class LeaderBase
    {
        public LeaderDatum[] data { get; set; }
        public LeaderPagination pagination { get; set; }
    }

    public class LeaderPagination
    {
        public int offset { get; set; }
        public int max { get; set; }
        public int size { get; set; }
        public object[] links { get; set; }
    }

    public class LeaderDatum
    {
        public int id { get; set; }
        public string weblink { get; set; }
        public string game { get; set; }
        public string category { get; set; }
        public object level { get; set; }
        public object platform { get; set; }
        public object region { get; set; }
        public object emulators { get; set; }
        public bool videoonly { get; set; }
        public object timing { get; set; }
        public Values values { get; set; }
        public Run[] runs { get; set; }
        public LeaderLink1[] links { get; set; }
    }

    public class Values
    {
    }

    public class Run
    {
        public int id { get; set; }
        public int place { get; set; }
        public LeaderRun run { get; set; }
    }

    public class LeaderRun
    {
        public string id { get; set; }
        public string weblink { get; set; }
        public string game { get; set; }
        //public object level { get; set; }
        public string category { get; set; }
        public Videos videos { get; set; }
        public string comment { get; set; }
        public Status status { get; set; }
        public Player[] players { get; set; }
        public string date { get; set; }
        //public DateTime submitted { get; set; }
        public Times times { get; set; }
        public System system { get; set; }
        //public object splits { get; set; }
        public Values1 values { get; set; }
    }

    public class Videos
    {
        public int id { get; set; }
        public LeaderLink[] links { get; set; }
    }

    public class LeaderLink
    {
        public int id { get; set;  }
        public string uri { get; set; }
    }

    public class Status
    {
        public int id { get; set;  }
        public string status { get; set; }
        public string examiner { get; set; }
        //public DateTime verifydate { get; set; }
    }

    public class Times
    {
        public int id { get; set; }
        public string primary { get; set; }
        public double primary_t { get; set; }
        public TimeSpan timeInMinutes
        {
            get
            {
                TimeSpan t = TimeSpan.FromSeconds(primary_t);
                return t; 

            }
        }
        public string realtime { get; set; }
        //public int realtime_t { get; set; }
        //public object realtime_noloads { get; set; }
        public int realtime_noloads_t { get; set; }
        //public object ingame { get; set; }
        public int ingame_t { get; set; }
    }

    public class System
    {
        public int id { get; set;  }
        public string platform { get; set; }
        public bool emulated { get; set; }
        public string region { get; set; }
    }

    public class Values1
    {
        public int id { get; set;  }
        public string e8m7em86 { get; set; }
        public string kn04ewol { get; set; }
    }

    public class Player
    {
        public string rel { get; set; }
        public string id { get; set; }
        public string uri { get; set; }
    }

    public class LeaderLink1
    {
        public int id { get; set; }
        public string rel { get; set; }
        public string uri { get; set; }
    }

}
