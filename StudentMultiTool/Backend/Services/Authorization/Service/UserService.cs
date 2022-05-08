using Microsoft.Extensions.Options;
using BCryptNet = BCrypt.Net.BCrypt;
using StudentMultiTool.Backend.Services.Authorization.Entities;
using StudentMultiTool.Backend.Services.Authorization.Helpers;
using StudentMultiTool.Backend.Models.Users;

namespace StudentMultiTool.Backend.Services.Authorization.Service
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.email == model.email);

            // validate
            if (user == null || !BCryptNet.Verify(model.email, user.passcode))
                throw new AppException("Email or passcode is incorrect");

            // authenticat
            // ion successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
