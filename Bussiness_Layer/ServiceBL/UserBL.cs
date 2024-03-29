﻿using Bussiness_Layer.InterfaceBL;
using Model_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.User_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.ServiceBL
{
    public class UserBL : IUserBL
    {
        public readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserEntity AddUserDetail(UserModel userModel)
        {
            return userRL.AddUserDetail(userModel);
        }

        public string Login(LoginModel loginModel)
        {
            return userRL.Login(loginModel);
        }

        public  Task<string> Forget_Password(string email)
        {
            return userRL.Forget_Password(email);
        }


        public bool PasswordReset(string newPassword, int userId)
        {
            return userRL.PasswordReset(newPassword, userId);   
        }





    }
}
