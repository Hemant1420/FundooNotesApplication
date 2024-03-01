using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.ContextClass
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions option) : base(option)
        {
            

        }
        public DbSet<UserEntity> Users  { get; set; }

    }
}
