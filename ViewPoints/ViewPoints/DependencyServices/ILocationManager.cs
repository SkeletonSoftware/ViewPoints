using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.Models;

namespace ViewPoints.DependencyServices
{
    /// <summary>
    /// Třída pro práci s polohou
    /// </summary>
    public interface ILocationManager
    {
        /// <summary>
        /// Metoda která vrátí aktální polohu zařízení, pomocí fused location
        /// </summary>
        /// <returns></returns>
        Task<Position> GetLocation();
    }
}
