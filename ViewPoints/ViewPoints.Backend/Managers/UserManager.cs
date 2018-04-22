using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewPoints.Backend.DataAccess.WebRepository;
using ViewPoints.Backend.Models;

namespace ViewPoints.Backend.Managers
{
    public class UserManager
    {
        internal static User CurrentUser { get; private set; }

        public async Task<bool> LoginUser(string name)
        {
            var user = new User()
            {
                Name = name,
            };
            var webRepository = new UserWebRepository();
            var id = await webRepository.GetUser(name);
            user.Id = id;
            CurrentUser = user;
            return true;
        }
    }
}
