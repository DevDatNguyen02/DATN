using Abp.Authorization;
using HotelMarriotter.Authorization.Roles;
using HotelMarriotter.Authorization.Users;

namespace HotelMarriotter.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
