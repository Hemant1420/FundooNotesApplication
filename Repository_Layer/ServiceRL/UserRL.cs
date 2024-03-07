using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model_Layer.Models;
using Repository_Layer.ContextClass;
using Repository_Layer.Entity;
using Repository_Layer.Hashing;
using Repository_Layer.JwtToken;
using Repository_Layer.User_Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.User_Service
{
    public class UserRL : IUserRL
    {
        private readonly UserContext _contextClass;
        private readonly IConfiguration _config;
        private readonly Hash_password _hash_Password;

        public UserRL(UserContext contextClass, IConfiguration config, Hash_password hash_Password)

        {
            _config = config;
            _contextClass = contextClass;
            _hash_Password = hash_Password;
        }

        public UserEntity AddUserDetail(UserModel userModel)
        {
            UserEntity user = new UserEntity();
            user.First_name = userModel.First_name;
            user.Last_name = userModel.Last_name;
            user.Email = userModel.Email;
            user.Password = _hash_Password.HashPassword(userModel.Password);

            _contextClass.Users.Add(user);
            _contextClass.SaveChanges();

            return user;
        }

        public string Login(LoginModel login)
        {
            UserEntity valid = null;
          

            valid = _contextClass.Users.FirstOrDefault(e => e.Email == login.Email );

            Jwt_Token token = new Jwt_Token(_config);
            if (valid != null)
            {
                bool pass = _hash_Password.VerifyPassword(login.Password, valid.Password);  //Check this(error if entered wrong email or password
                if (pass)
                {
                    return token.GenerateToken(valid);
                }
            }
            return  null;

        }

      
       







    }
}
