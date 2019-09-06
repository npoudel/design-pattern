using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class CityRepository
    {
        private readonly ContactdbContext context;
        public CityRepository(ContactdbContext context)
        {
            this.context = context;
        }
        public void Add(City city)
        {
            context.City.Add(city);
        }
        public void Update(City city)
        {
            context.City.Update(city);
        }
         
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
