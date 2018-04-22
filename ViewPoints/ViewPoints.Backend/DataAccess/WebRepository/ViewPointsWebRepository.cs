using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.DataAccess.WebRepository.Models;
using ViewPoints.Backend.Models;
using ViewPoints.Backend.Tools;

namespace ViewPoints.Backend.DataAccess.WebRepository
{
    class ViewPointsWebRepository
    {
        public async Task<List<ViewPoint>> GetViewPoints(int userId)
        {
            var restManager = new RestManager<List<ViewPointsOutput>>(Configuration.ServiceUrl);
            var result = await restManager.SendGetRequest("viewpoints", string.Empty);
            return this.LoadResult(result);
        }

        private List<ViewPoint> LoadResult(List<ViewPointsOutput> result)
        {
            var output = new List<ViewPoint>(result.Count);
            foreach (var item in result)
            {
                output.Add(item);
            }
            return output;
        }
    }
}
