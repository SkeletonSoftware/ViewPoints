using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.DataAccess.WebRepository.Models;
using ViewPoints.Backend.Tools;

namespace ViewPoints.Backend.DataAccess.WebRepository
{
    class UserWebRepository
    {
        public async Task<int> GetUser(string name)
        {
            var restManager = new RestManager<UserModel>(Configuration.ServiceUrl);
            var result = await restManager.SendPostRequest("users", new UserModel()
            {
                Name = name
            });
            return result.Id.Value;
        }
    }
}
