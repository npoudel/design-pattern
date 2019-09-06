using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; 
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Utilities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class AccountController : Controller
    {
        //this is account controller.
        public IAccountService _account;
        public IConfiguration _config;
        public IHttpContextAccessor _httpContext;
        public ContactdbContext _context;
        public AccountController(IAccountService account, IConfiguration config, IHttpContextAccessor httpContext,ContactdbContext context)
        {
            _account = account;
            _config = config;
            _httpContext = httpContext;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost, ActionName("RegisterUser")]
        public JsonResult RegisterUser([FromBody]ApplicationUsers users)
        {
            users.Pass = Guid.NewGuid().ToString();
            users.Password = PasswordSecurity.Encrypt(users.Password, users.Pass);
            var result = _account.RegisterAccount(users);            
            return new JsonResult("Success");
        }

        [AllowAnonymous]
        [HttpPost, ActionName("ValidateUser")]
        public JsonResult ValidateUser(ApplicationUsers users)
        {
            var token = "";
            var user = _account.LoginUser(users);
            if (user!=null)
            {
                token= GenerateToken(user);
            }
            return new JsonResult(new {  msg = "login successful",token=token });
        }

        [Authorize]
        [HttpPost, ActionName("AddEditRoles")]
        public IActionResult AddEditRoles(ApplicationRoles roles)
        {
            var result = _account.AddEditUserRoles(roles,_httpContext);
            return new JsonResult("success");
        }
        private string GenerateToken(ApplicationUsers userInfo)
        {
            var RoleName = _context.ApplicationRoles.Where(a => a.Id == userInfo.RoleId).FirstOrDefault();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.UserName),
                    new Claim(ClaimTypes.Role,RoleName.RoleName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}