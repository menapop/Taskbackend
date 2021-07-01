using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Helpers.Auth;
using Service.Helpers.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers.Filters
{
 
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TokenFilter : AuthorizeAttribute, IAuthorizationFilter
    {
      
        private ITokenHandler _tokenHandler;
        private IAuthService _authService;
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            _tokenHandler = (ITokenHandler) context.HttpContext.RequestServices.GetService(typeof(ITokenHandler));
            _authService = (IAuthService)context.HttpContext.RequestServices.GetService(typeof(IAuthService));

            try
            {
                var token = context.HttpContext.Request.Headers["Authorization"];
                var Claims = _tokenHandler.GetTokenClaims(token.ToString().Replace("Bearer ","")).ToList();
                _authService.UserId = Guid.Parse(Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.Sub).Value);
                _authService.Email = Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.Email).Value;
                _authService.UserName = Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.GivenName).Value;
                _authService.Roles = Claims.FirstOrDefault(cl => cl.Type == "Roles").Value.Split(",");
            }
            catch(Exception ex)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);

            }

        }
    }
}
