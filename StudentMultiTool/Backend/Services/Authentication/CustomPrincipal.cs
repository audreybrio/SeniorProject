using System.Security.Principal;

namespace StudentMultiTool.Backend.Services.Authentication
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; set; } 
        public bool IsInRole(string role)
        {
            return false;
        }

        public CustomPrincipal(string email)
        {
            this.Identity = new GenericIdentity(email); 
        }
        public int Id { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public string email { get; set; }
    }

}
