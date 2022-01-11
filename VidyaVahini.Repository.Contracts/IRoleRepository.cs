using System.Collections.Generic;
using VidyaVahini.Entities.Role;

namespace VidyaVahini.Repository.Contracts
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Gets the role assigned to a user
        /// </summary>
        /// <param name="userId">Id</param>
        /// <returns>Role details</returns>
        IEnumerable<RoleData> GetUserRoles(string userId);

        /// <summary>
        /// Assign a new role to a user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="roleId">Role Id</param>
        void AddUserRole(string userId, int roleId);
    }
}
