﻿using Microsoft.AspNetCore.Http;
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
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }


         [HttpPost("Register")]
        public ResponseModel<UserModel> AddUser(UserModel userModel)
        {
            var response = new ResponseModel<UserModel>();

            var Result = userBL.AddUserDetail(userModel);

            if (Result != null)
            {
                response.IsSuccess = true;
                response.Message = "User Registered successfully";
                response.Data = userModel;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "User Registeration failed!, Please try again";
                
            }
            return response;
        }

        [HttpPost("Login")]
       
        public ResponseModel<LoginModel> Login( LoginModel loginModel)
        {
            var response = new ResponseModel<LoginModel>();

            var Result = userBL.Login(loginModel);

            if (Result != null)
            {
                response.IsSuccess = true;
                response.Message = "User Login successful";
                response.Data = loginModel;
            }
            else
            {
                response.IsSuccess = true;
                response.Message = "User Login failed, Please enter the valid credentials";
            }
            return response;


        }
      

       

      

    }
}