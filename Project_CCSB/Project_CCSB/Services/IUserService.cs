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

        public Task<string> GetUserEmail();

        /// <summary>
        /// Gets a list of roles tied to the user
        /// </summary>
        /// <returns>A list of strings with user roles</returns>
        public Task<List<string>> GetUserRoles();
    }
}
