using Microsoft.AspNetCore.Authorization;
using Sunshine.Enums;

namespace Sunshine.Attributes
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private Role roleEnum;
        public Role RoleEnum
        {
            get { return roleEnum; }
            set 
            { 
                roleEnum = value;
                var a = value.ToString();
                base.Roles = a; 
            }
        }
    }
}
