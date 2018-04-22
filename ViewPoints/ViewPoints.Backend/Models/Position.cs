using System;
using System.Collections.Generic;
using System.Text;

namespace ViewPoints.Backend.Models
{
    public class Position
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float? Altitude { get; set; }
    }
}
