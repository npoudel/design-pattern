using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Utilities;

namespace WebApplication1.Services
{
    public class AccountService : IAccountService
    {
        private ContactdbContext _context;
        public AccountService(ContactdbContext context)
        {
            _context = context;
        }
        public ApplicationUsers LoginUser(ApplicationUsers users)
        {
            try
            {
                var user = _context.ApplicationUsers.Where(a => a.UserName == users.UserName).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }
                else
                {
                    var passwordKey = user.Pass;
                    var password = user.Password;
                    var resultDecrypt = PasswordSecurity.Decrypt(password, passwordKey);
                    if (resultDecrypt == users.Password)
                    {
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public string RegisterAccount(ApplicationUsers users)
        {
            try
            {
                ApplicationUsers applicationUsers = new ApplicationUsers();
                applicationUsers.Pass = users.Pass;
                applicationUsers.Password = users.Password;
                applicationUsers.UserName = users.UserName;
                applicationUsers.RoleId = users.RoleId;
                _context.ApplicationUsers.Add(applicationUsers);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        public string AddEditUserRoles(ApplicationRoles roles, IHttpContextAccessor contextAccessor)
        {
            
            try
            {
                if (roles.Id == 0)
                {
                    ApplicationRoles application = new ApplicationRoles();
                    application.RoleName = roles.RoleName;
                    application.Description = roles.Description;
                    _context.ApplicationRoles.Add(application);
                    _context.SaveChanges();
                }
                else
                {
                    _context.ApplicationRoles.Update(roles);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
            return "";
        }
    }
}
