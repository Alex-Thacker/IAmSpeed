using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAmSpeed.Models
{

    public class CategoriesBase
    {
        public CategoriesDatum[] data { get; set; }
    }

    public class CategoriesDatum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string weblink { get; set; }
        public string type { get; set; }
        public string rules { get; set; }
        public Players players { get; set; }
        public bool miscellaneous { get; set; }
        public Link[] links { get; set; }
    }

    public class Players
    {
        public string type { get; set; }
        public int value { get; set; }
    }

    public class CategoriesLink
    {
        public string rel { get; set; }
        public string uri { get; set; }
    }

}
