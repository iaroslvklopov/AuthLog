using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AuthLog
{
    public class UserContext : DbContext
    {
        private static UserContext userContext = null;
        public UserContext() : base("DbConnection") { }
        public static UserContext GetContext()
        {
            if(userContext == null)
            {
                userContext = new UserContext();
            }
            return userContext;
        }
        public DbSet<User> Users { get; set; }
    }

}
