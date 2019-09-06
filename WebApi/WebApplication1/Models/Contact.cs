using System;
using System.Collections.Generic;
using WebApplication1.Services;

namespace WebApplication1.Models
{
    public partial class Contact:BaseEntity
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? City { get; set; }
        public string PhoneNumber { get; set; }
    }
}
