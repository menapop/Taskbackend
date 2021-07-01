using Data.Entities;
using DTOS;
using Repo;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Service.Helpers.Token
{
   public  interface ITokenHandler
    {
        OutputRefreshTokenDto GetToken(User user);

        IEnumerable<Claim> GetTokenClaims(string token);
    }
}
