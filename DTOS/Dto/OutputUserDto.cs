using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS.Dto
{
    public class OutputUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public List<int> Roles { get; set; }
    }
}
