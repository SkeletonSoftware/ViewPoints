using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.Models;

namespace ViewPoints.DependencyServices
{
    public interface ILocationManager
    {
        Task<Position> GetLocation();
    }
}
