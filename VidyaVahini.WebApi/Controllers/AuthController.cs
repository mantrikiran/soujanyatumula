using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using VidyaVahini.Core.Constant;
using VidyaVahini.Entities.UserAccount;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IUserAccountService _userAccountService;

        public AuthController(
            IConfiguration config,
            IUserAccountService userAccountService)
        {
            _config = config;
            _userAccountService = userAccountService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody]LoginCommand login)
        {
            IActionResult response = Unauthorized();

            var user = _userAccountService.AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [Route("[action]")]
        [HttpPost]
        public MetorData MentorProfile([FromBody] MentorProfileCommand mpc)
        {
            var user = _userAccountService.MentorProfile(mpc);
            return user;
        }
        private string GenerateJSONWebToken(UserModel userDetails)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: GetClaims(userDetails),
              expires: DateTime.Now.AddMinutes(Constants.AuthTokenExpiry),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetClaims(UserModel userDetails)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userDetails.UserId));

            claims.Add(new Claim(Constants.NameClaim, userDetails.Name));

            claims.Add(new Claim(Constants.EmailClaim, userDetails.Email));

            claims.Add(new Claim(Constants.UserDataClaim, JsonConvert.SerializeObject(userDetails.UserData)));

            claims.AddRange(
                userDetails
                .UserRoles
                .Select(x => new Claim(ClaimTypes.Role, x.Id.ToString())));

            claims.AddRange(
                userDetails
                .UserRoles
                .Select(x => new Claim(Constants.RoleIdClaim, x.Id.ToString())));

            return claims;
        }
    }
}