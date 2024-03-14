using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model_Layer.Models;
using Repository_Layer.ContextClass;
using Repository_Layer.Entity;
using Repository_Layer.Hashing;
using Repository_Layer.JwtToken;
using Repository_Layer.ServiceRL;
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

        public string Login(LoginModel loginModel)
        {
            //UserEntity valid = null;
          

           var valid = _contextClass.Users.FirstOrDefault(e => e.Email == loginModel.Email );

            Jwt_Token token = new Jwt_Token(_config);
            if (valid != null)
            {
                bool pass = _hash_Password.VerifyPassword(loginModel.Password, valid.Password);  //Check this(error if entered wrong email or password
                if (pass)
                {
                    return token.GenerateToken(valid);
                }
            }
            return  null;

        }

        public async Task<string?> Forget_Password(string email)
        {
            var user = _contextClass.Users.FirstOrDefault(e => e.Email == email);
            Jwt_Token token = new Jwt_Token(_config);


            if (user != null)
            {
                string _token = token.GenerateTokenReset(user.Email,user.Id);

              
                // Generate password reset link
                var callbackUrl = $"https://localhost:7258/api/User/ResetPassword?token={_token}";

                // Send password reset email
                EmailService _emailService = new EmailService();
                await _emailService.SendEmailAsync(email,"Reset Password", callbackUrl);
                return "Ok";
            }
            else
            {
                return null;

            }
           
            
        }

        public bool PasswordReset(string newPassword, int userId)
        {
            var User = _contextClass.Users.FirstOrDefault(e => e.Id == userId);

            if(User != null)
            {
                string newPass = _hash_Password.HashPassword(newPassword);
                User.Password = newPass;    
                _contextClass.SaveChanges();
                return true;
            }
            return false;


        }


      
       







    }
}
