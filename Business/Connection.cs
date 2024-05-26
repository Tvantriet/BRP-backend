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
        public int Distance { get; set; }
        public int Duration { get; set; }
        public int Direction { get; set; }
    }
}
