﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Maps.Routing.V2;
using Google.Protobuf.WellKnownTypes;

namespace Business
{
    public class Route
    {
        public int Id { get; set; }
        public City Departure { get; set; }
        public City Destination { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double Direction { get; set; }
    }
}
