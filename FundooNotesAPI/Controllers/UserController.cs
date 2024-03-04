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

namespace FundooNotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }


        

     



        [HttpPost]

        public IActionResult AddUser(UserModel userModel)
        {
            var Result = userBL.AddUserDetail(userModel);
            if (Result != null)
            {
                return Ok(new { Success = true, Message = "User Registered Successfully", Data = Result });
            }
            else
            {
                return BadRequest(new { Successs = false, Message = "Something Went wrong,Please try again! " });
            }
        }

        [HttpPost]
       [Route("Login")]

        
        public IActionResult Login( LoginModel loginModel)
        {
            var Result = userBL.Login(loginModel);

            if (Result != null)
            {
                return Ok(new { Success = true, Message = "User Login Successfully", Data = Result });
            }
            else
            {
                return BadRequest(new { Successs = false, Message = "Something Went wrong,Please try again! " });
            }


        }
      

       

      

    }
}
