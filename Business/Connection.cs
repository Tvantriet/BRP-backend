using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Connection
    {
        public int Id { get; set; }
        public City Departure { get; set; }
        public City Destination { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double Direction { get; set; }
    }
}
