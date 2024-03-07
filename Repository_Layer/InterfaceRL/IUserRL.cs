using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.User_Interface
{
    public interface IUserRL
    {

        public UserEntity AddUserDetail(UserModel userModel);

        public string Login(LoginModel login);

    }

}
