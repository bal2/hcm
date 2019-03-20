using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using hcm.Database;
using hcm.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace hcm.Auth
{
    [Route("api/auth"), ApiController]
    public class AuthController : Controller
    {

        private HcmContext _dbContext;

        public AuthController(HcmContext context)
        {
            this._dbContext = context;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            if (credentials == null)
                return BadRequest("Invalid credentials");

            var user = _dbContext.Users.Where(u => u.Email == credentials.Email).FirstOrDefault();

            if (user == null)
                return BadRequest("Invalid credentials");

            var hasher = new PasswordHasher<User>();

            if (hasher.VerifyHashedPassword(user, user.PasswordHash, credentials.Password) != PasswordVerificationResult.Success)
                return BadRequest("Invalid credentials");

            //Get permissions
            var query = from roleUsers in _dbContext.RoleUsers
                        join rolePermissions in _dbContext.RolePermissions on roleUsers.RoleId equals rolePermissions.RoleId
                        join permissions in _dbContext.Permissions on rolePermissions.PermissionId equals permissions.PermissionId
                        where roleUsers.UserId == user.UserId
                        select permissions;

            string permissionString = "";
            List<Permission> prm = query.ToList();
            foreach (Permission p in prm)
                permissionString += p.Name + " ";

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(CustomClaimTypes.Permissions, permissionString)
            };

            //TODO
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.Now.AddMinutes(60);
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                expires: expirationTime,
                signingCredentials: signinCredentials
            );

            return Ok(new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), expirationTime));
        }

    }

    public class TokenResponse
    {
        public string Token { get; private set; }
        public DateTime ExpirationTime { get; private set; }

        public TokenResponse(string token, DateTime expirationTime)
        {
            Token = token;
            ExpirationTime = expirationTime;
        }
    }

    public static class CustomClaimTypes
    {
        public const string Permissions = "Permissions";
    }
}