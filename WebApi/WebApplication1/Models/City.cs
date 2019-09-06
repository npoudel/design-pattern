using System;
using System.Collections.Generic;
using WebApplication1.Services;

namespace WebApplication1.Models
{
    public partial class City:BaseEntity
    {
       // public int Id { get; set; }
        public string CityName { get; set; }
        public string Description { get; set; }
    }
}
