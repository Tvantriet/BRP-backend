using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Route : DBEntity
    {
        public string name { get; set; }
        public double startLat { get; set; }
        public double startLong { get; set; }
        public string? startCity { get; set; }
        public double endLat { get; set; }
        public double endLong { get; set; }
        public string? endCity { get; set; }
        public double distance { get; set; }

    }
}
