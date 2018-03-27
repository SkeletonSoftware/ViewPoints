﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ViewPoints.Backend.Models
{
    public class ViewPoint
    {
        public string Title { get; set; }

        public Position Location { get; set; }

        public float TowerHeight { get; set; }

        public string Description { get; set; }

        public string OpeningHours { get; set; }
    }
}
