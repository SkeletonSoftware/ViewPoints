using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewPoints.Controls.Models;

namespace ViewPoints.Controls
{
    /// <summary>
    /// vlastni, vylepsena mapa
    /// </summary>
    public class Map : Xamarin.Forms.Maps.Map
    {
        private ObservableCollection<CustomPin> pins;

        public Map()
        {
            pins = new ObservableCollection<CustomPin>();
        }

        public ObservableCollection<CustomPin> CustomPins
        {
            get { return this.pins; }
        }
    }
}
