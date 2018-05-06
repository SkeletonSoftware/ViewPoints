using System;
using System.Collections.Generic;
using System.Text;
using ViewPoints.Backend.Models;
using Xamarin.Forms;

namespace ViewPoints.Controls.Models
{
    /// <summary>
    /// Trida reprezentujici vlastni pin na mape
    /// </summary>
    public class CustomPin
    {
        public Position Location { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Color Color { get; set; }
    }
}
