using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public partial class ApplicationUsers
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Pass { get; set; }
        public int? RoleId { get; set; }
    }
}
