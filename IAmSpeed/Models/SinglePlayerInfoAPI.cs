using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{

    public class SinglePlayerBase
    {
        public SinglePlayerData data { get; set; }
    }

    public class SinglePlayerData
    {
        public string id { get; set; }
        public SinglePlayerNames names { get; set; }
        public string weblink { get; set; }
        public NameStyle namestyle { get; set; }
        public string role { get; set; }
        public DateTime signup { get; set; }
        public Location location { get; set; }
        public Twitch twitch { get; set; }
        public object hitbox { get; set; }
        public Youtube youtube { get; set; }
        public Twitter twitter { get; set; }
        public object speedrunslive { get; set; }
        public SinglePlayerLink[] links { get; set; }
    }

    public class SinglePlayerNames
    {
        public string international { get; set; }
        public object japanese { get; set; }
    }

    public class NameStyle
    {
        public string style { get; set; }
        public ColorFrom colorfrom { get; set; }
        public ColorTo colorto { get; set; }
    }

    public class ColorFrom
    {
        public string light { get; set; }
        public string dark { get; set; }
    }

    public class ColorTo
    {
        public string light { get; set; }
        public string dark { get; set; }
    }

    public class Location
    {
        public Country country { get; set; }
        public Region region { get; set; }
    }

    public class Country
    {
        public string code { get; set; }
        public Names1 names { get; set; }
    }

    public class Names1
    {
        public string international { get; set; }
        public object japanese { get; set; }
    }

    public class Region
    {
        public string code { get; set; }
        public Names2 names { get; set; }
    }

    public class Names2
    {
        public string international { get; set; }
        public object japanese { get; set; }
    }

    public class Twitch
    {
        public string uri { get; set; }
    }

    public class Youtube
    {
        public string uri { get; set; }
    }

    public class Twitter
    {
        public string uri { get; set; }
    }

    public class SinglePlayerLink
    {
        public string rel { get; set; }
        public string uri { get; set; }
    }

}
