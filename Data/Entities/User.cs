using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
        public string UserName { get; set; }

        public string Mobile { get; set; }

        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }
      
       public virtual ICollection<UserRole> UserRoles { get; set; }
       
    }
}
