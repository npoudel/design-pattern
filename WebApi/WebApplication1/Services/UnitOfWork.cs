using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UnitOfWork
    {
        private ContactdbContext _context;
        private CityRepository cityRepository;
        private ContactRepository contactRepository;
        public UnitOfWork(ContactdbContext context)
        {
            _context = context;
        }
        public CityRepository CityRepository
        {
            get
            {
                if (cityRepository == null)
                {
                    cityRepository = new CityRepository(_context);
                }
                return cityRepository;
            }
        }
        public ContactRepository ContactRepository
        {
            get
            {
                if (contactRepository == null)
                {
                    contactRepository = new ContactRepository(_context);
                }
                return contactRepository;
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
