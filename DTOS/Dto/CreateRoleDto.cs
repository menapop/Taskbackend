using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS.Dto
{
    public class CreateRoleDto
    {
        public string Name { get; set; }
        public  List<int> permissions { get; set; }
        public bool HasFullAccess { get; set; }
    }
}
