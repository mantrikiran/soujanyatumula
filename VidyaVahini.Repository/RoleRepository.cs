using System;
using System.Collections.Generic;
using System.Linq;
using VidyaVahini.DataAccess.Contracts;
using VidyaVahini.DataAccess.Models;
using VidyaVahini.Entities.Role;
using VidyaVahini.Repository.Contracts;

namespace VidyaVahini.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataAccessRepository<UserRole> _userRole;

        public RoleRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRole = _unitOfWork.Repository<UserRole>();
        }

        public IEnumerable<RoleData> GetUserRoles(string userId)
            => _userRole
            .Filter(x => string.Equals(x.UserId, userId, StringComparison.OrdinalIgnoreCase), includeProperties: nameof(Role))
            .Select(x => new RoleData
            {
                Id = x.RoleId,
                Name = x.Role.RoleDescription
            });

        public void AddUserRole(string userId, int roleId)
            => _userRole.Add(new UserRole
            {
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                RoleId = roleId,
                UserId = userId
            });
    }
}
