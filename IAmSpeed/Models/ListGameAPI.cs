using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{

    public class ListGameBase
    {
        public Datum[] data { get; set; }
        public Pagination pagination { get; set; }
        public List<Game> games { get; set; } = new List<Game>();
    }

    public class SingleGameBase
    {
        public Datum data { get; set; }
    }

    public class Pagination
    {
        public int offset { get; set; }
        public int offsetPlus
        {
            get
            {
                return offset + 1;
            }
        }
        public int max { get; set; }
        public int size { get; set; }
        public Link[] links { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string uri { get; set; }
    }
    //[DataContract]
    public class Datum
    {
        public string id { get; set; }
        public Names names { get; set; }
        public string abbreviation { get; set; }
        public string weblink { get; set; }
        public int released { get; set; }
        //[DataMember(Name = "release-date")]
        public string releasedate { get; set; }
        public Ruleset ruleset { get; set; }
        public bool romhack { get; set; }
        public string[] gametypes { get; set; }
        public string[] platforms { get; set; }
        public string[] regions { get; set; }
        public string[] genres { get; set; }
        public string[] engines { get; set; }
        public string[] developers { get; set; }
        public string[] publishers { get; set; }
        public Moderators moderators { get; set; }
        public DateTime? created { get; set; }
        public Assets assets { get; set; }
        public Link1[] links { get; set; }
    }

    public class Names
    {
        public string international { get; set; }
        public string japanese { get; set; }
        public string twitch { get; set; }
    }

    public class Ruleset
    {
        public bool showmilliseconds { get; set; }
        public bool requireverification { get; set; }
        public bool requirevideo { get; set; }
        public string[] runtimes { get; set; }
        public string defaulttime { get; set; }
        public bool emulatorsallowed { get; set; }
    }

    public class Moderators
    {
        public string qjnzw4jm { get; set; }
        public string kj9r2rj4 { get; set; }
        public string qjn91qxm { get; set; }
        public string qj2n06jk { get; set; }
        public string _1xy51wv8 { get; set; }
        public string qxk43g6j { get; set; }
        public string y8d3y0mx { get; set; }
        public string _98r3qn68 { get; set; }
        public string qxk10d2j { get; set; }
        public string qjonw238 { get; set; }
        public string qxk60398 { get; set; }
        public string v8l6lwv8 { get; set; }
        public string qjn1ozw8 { get; set; }
        public string _68w5lgq8 { get; set; }
        public string qxkog680 { get; set; }
        public string _68wl23jg { get; set; }
        public string o86n2pxz { get; set; }
        public string _68w15lxg { get; set; }
        public string qjn2k4jm { get; set; }
        public string _18qpqwxn { get; set; }
        public string _68wk7yz8 { get; set; }
        public string kjprzz08 { get; set; }
        public string _98r701dj { get; set; }
        public string qjop1ex6 { get; set; }
        public string _18qrdojn { get; set; }
        public string y8d4yl86 { get; set; }
        public string kjpw2kxq { get; set; }
        public string qjoon4lj { get; set; }
        public string _68wz2v8g { get; set; }
        public string y8d4l798 { get; set; }
        public string v813edlx { get; set; }
        public string v8lyv4jm { get; set; }
        public string _18v52d5j { get; set; }
        public string pj0n70m8 { get; set; }
        public string qjopronx { get; set; }
        public string qxkm6d2j { get; set; }
        public string _18qy0qxn { get; set; }
        public string _98rpnqdj { get; set; }
        public string e8egp16x { get; set; }
        public string kj9d03ox { get; set; }
        public string e8e23od8 { get; set; }
        public string pj0n2qr8 { get; set; }
        public string qxkmo2kj { get; set; }
        public string qjn0yowx { get; set; }
        public string o86v9pjz { get; set; }
        public string kj96k7j4 { get; set; }
        public string qjogonx6 { get; set; }
        public string qjonkyl8 { get; set; }
        public string v8lk74lx { get; set; }
        public string qxko1m80 { get; set; }
        public string qjn3w4xm { get; set; }
        public string zx7730x7 { get; set; }
        public string dx3nykjl { get; set; }
        public string _1xyrqdwj { get; set; }
        public string qj2q6doj { get; set; }
        public string _7j406d81 { get; set; }
        public string _1xykkm8r { get; set; }
        public string _7j4rrnl8 { get; set; }
        public string dx35zdej { get; set; }
        public string y8d4kg86 { get; set; }
        public string y8dem0mj { get; set; }
        public string _18vrlk5x { get; set; }
        public string y8d4d0g8 { get; set; }
        public string qjnpdnq8 { get; set; }
        public string kjpmg4jq { get; set; }
        public string qxko4668 { get; set; }
        public string _1xy5k0y8 { get; set; }
        public string _5j52r5zj { get; set; }
        public string _18vrv7yx { get; set; }
        public string kjpg5p2j { get; set; }
        public string _1xy5mzz8 { get; set; }
        public string pj061l4j { get; set; }
    }
    [DataContract]
    public class Assets
    {
        public Logo logo { get; set; }
        [DataMember(Name = "cover-tiny")]
        public CoverTiny covertiny { get; set; }
        [DataMember(Name = "cover-small")]
        public CoverSmall coversmall { get; set; }
        [DataMember(Name = "cover-medium")]
        public CoverMedium covermedium { get; set; }
        [DataMember(Name = "cover-large")]
        public CoverLarge coverlarge { get; set; }
        public Icon icon { get; set; }
        public Trophy1St trophy1st { get; set; }
        public Trophy2Nd trophy2nd { get; set; }
        public Trophy3Rd trophy3rd { get; set; }
        public Trophy4Th trophy4th { get; set; }
        public Background background { get; set; }
        public object foreground { get; set; }
    }

    public class Logo
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class CoverTiny
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class CoverSmall
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class CoverMedium
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class CoverLarge
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Icon
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Trophy1St
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Trophy2Nd
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Trophy3Rd
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Trophy4Th
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Background
    {
        public string uri { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Link1
    {
        public string rel { get; set; }
        public string uri { get; set; }
    }

}
