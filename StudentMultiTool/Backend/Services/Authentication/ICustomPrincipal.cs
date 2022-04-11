using System.Security.Principal;

namespace StudentMultiTool.Backend.Services.Authentication
{
    public interface ICustomPrincipal: IPrincipal
    {
        int Id { get; set; }
        string username { get; set; }
        string role { get; set; }
        string email { get; set; }
    }
}
