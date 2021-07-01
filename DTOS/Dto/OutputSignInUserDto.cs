using System;
using System.Collections.Generic;
using System.Text;

namespace DTOS.Dto
{
    public class OutputSignInUserDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Name { get; set; }
    }
}
