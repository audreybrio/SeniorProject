using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserAcc;

namespace StudentMultiTool.Backend.Services.Authorization
{
    public class AuthorizationAttributes: IAuthorizationFilter
    {
        private readonly string _actionName;
        private readonly string _roleType;

        UserAccount ur = new UserAccount();
        public AuthorizationAttributes(string actionName, string roleType)
        {
            _actionName = actionName;
            _roleType = roleType;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string _roleType = context.HttpContext.Request?.Headers["role"].ToString();
            switch (_actionName)
            {
                case "Index":
                    if (!_roleType.Contains("admin")) context.Result = new JsonResult("Permission denined!");
                    break;
            }
        }
    }
}
