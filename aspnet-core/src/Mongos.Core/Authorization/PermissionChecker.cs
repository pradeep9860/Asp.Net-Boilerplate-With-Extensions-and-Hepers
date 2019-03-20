using Abp.Authorization;
using Mongos.Authorization.Roles;
using Mongos.Authorization.Users;

namespace Mongos.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
