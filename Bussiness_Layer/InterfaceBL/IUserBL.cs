using Model_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.InterfaceBL
{
    public interface IUserBL
    {
        public UserEntity AddUserDetail(UserModel userModel);

        public string Login(LoginModel login);





    }
}
