using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Gets the current users id using IHttpContextAccessor
        /// </summary>
        /// <returns>String of users id</returns>
        public string GetUserId();

        public Task<List<string>> GetUserRoles();
    }
}
