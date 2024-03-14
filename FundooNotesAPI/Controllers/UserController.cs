using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_Layer.Models;
using Bussiness_Layer.InterfaceBL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.ContextClass;
using Microsoft.Identity.Client;
using Repository_Layer.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Azure;

namespace FundooNotesAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly IConfiguration _config;

        public UserController(IUserBL userBL, IConfiguration config)
        {
            this.userBL = userBL;
            _config = config;
        }


         [HttpPost]
        public ResponseModel<UserModel> AddUser(UserModel userModel)
        {
            var response = new ResponseModel<UserModel>();

            var Result = userBL.AddUserDetail(userModel);

            if (Result != null)
            {
                response.Success = true;
                response.Message = "User Registered successfully";
                response.Data = userModel;
            }
            else
            {
                response.Success = false;
                response.Message = "User Registeration failed!, Please try again";
                
            }
            return response;
        }

        [HttpPost("Login")]
       
        public ResponseModel<string> Login(LoginModel loginModel)
        {
            var response = new ResponseModel<string>();

            var Result = userBL.Login(loginModel);

            if (Result != null)
            {
                response.Success = true;
                response.Message = "User Login successful";
                response.Data = Result;
            }
            else
            {
                response.Success = false;
                response.Message = "User Login failed, Please enter the valid credentials";
            }
            return response;


        }

        [HttpPost("ForgetPassword")]
        
        public  async Task<ResponseModel<string>> ForgotPassword(string email)
        {
            var response = new ResponseModel<string>(); 

            var result = await userBL.Forget_Password(email);

            if (result != null)
            {
                response.Success = true;
                response.Message = "Reset password link sent successfully to your email address " + result;
            }
            else
            {
                response.Success = false;
                response.Message = "Unexpected error Occured ,Please Try again";
            }
            return response;
        }

        [HttpPost("ResetPassword")]
        
        public ResponseModel<bool> ResetPassword(string token,string password)
        {
            var response = new ResponseModel<bool>();

            try
            {
                // Validate token
                var handler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["JWT:Issuer"],
                    ValidAudience = _config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]))
                };

                SecurityToken validatedToken;
                var principal = handler.ValidateToken(token, validationParameters, out validatedToken);

                // Extract claims
                var userId = principal.FindFirstValue("UserId");
                int _userId = Convert.ToInt32(userId);

                // Perform operation (reset password) using userId
                // Note: Replace this with your actual password reset logic
                var result = userBL.PasswordReset(password, _userId);

                if (result)
                {
                    response.Success = true;
                    response.Message = "Password reset successful";
                    response.Data = result;
                }
                else
                {
                    response.Success = false;
                    response.Message = "An unexpected error occurred. Please try again.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error resetting password: " + ex.Message;
            }

            return response;
        }
    }
      

       

      

    }

