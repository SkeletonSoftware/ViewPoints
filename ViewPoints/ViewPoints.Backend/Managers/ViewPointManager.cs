using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.Models;

namespace ViewPoints.Backend.Managers
{
    public class ViewPointManager
    {
        private static List<ViewPoint> viewPoints = new List<ViewPoint>();

        public async Task<bool> AddViewPoint(ViewPoint viewPoint)
        {
            try
            {
                viewPoints.Add(viewPoint);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<List<ViewPoint>> GetViewPoints()
        {
            return viewPoints;
        }
    }
}
