using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ContactRepository
    {
        private readonly ContactdbContext context;
        
       // string errorMessage = string.Empty;
        public ContactRepository(ContactdbContext context)
        {
            this.context = context;
        }
        public void Add(Contact contact)
        {
           context.Contact.Add(contact);
        }
        public void Update(Contact contact)
        {
            context.Contact.Update(contact);
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
