using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model_Layer.Models;
using Bussiness_Layer.InterfaceBL;

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

    }
}
