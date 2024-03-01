using Model_Layer.Models;
using Repository_Layer.ContextClass;
using Repository_Layer.Entity;
using Repository_Layer.User_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.User_Service
{
    public class UserRL : IUserRL
    {
        private readonly UserContext _contextClass;

        public UserRL(UserContext contextClass)
        {
            _contextClass = contextClass;
        }

        public UserEntity AddUserDetail(UserModel userModel)
        {
            UserEntity user = new UserEntity();
            user.First_name = userModel.First_name;
            user.Last_name = userModel.Last_name;
            user.Email = userModel.Email;
            user.Password = userModel.Password;

            _contextClass.Users.Add(user);
            _contextClass.SaveChanges();


            return user;
        }

    }
}
