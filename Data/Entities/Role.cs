﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Role :BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string Name { get; set; }
     
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
