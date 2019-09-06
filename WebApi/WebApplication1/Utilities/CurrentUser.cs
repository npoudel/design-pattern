using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.Utilities
{
    public static class CurrentUser
    {
        public static string GetCurrentUser(IHttpContextAccessor httpContext)
        {
            var userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
        public static string GetUserRoles(IHttpContextAccessor httpContext)
        {
            var userRoles = httpContext.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
            return userRoles;
        }
    }
}
