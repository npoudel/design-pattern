using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Utilities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class ContactController : Controller
    {
        public IHttpContextAccessor _httpContext;
        private IRepository<Contact> _repoContact;
        private IRepository<City> _repoCity;
        public ContactController(IHttpContextAccessor httpContext,IRepository<Contact> repoContact,IRepository<City> repoCity)
        {
            _httpContext = httpContext;
            _repoContact = repoContact;
            _repoCity = repoCity;           
        }
        [Authorize]
        [HttpGet]
        public JsonResult GetUserNameRoles()
        {
            string userName = CurrentUser.GetCurrentUser(_httpContext);
            string userRoles = CurrentUser.GetUserRoles(_httpContext);
            return Json("success");
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public JsonResult OnlyAdmin()
        {
            return Json("success");
        }
        //[Authorize]
        [HttpPost]
        public JsonResult PostContactDetails([FromBody] Contact contact)
        {
            
            try
            {
                if (contact.Id == 0)
                {
                _repoContact.Insert(contact);
                }
                else
                {
                    _repoContact.Update(contact);
                }
            }
            catch (Exception ex)
            {

            }
            return Json("Success");
        }
        [HttpGet]
        public JsonResult GetAllContacts()
        {
           var result=  _repoContact.GetAll();
            return new JsonResult(result);
        }
        [HttpGet]
        public JsonResult GetAllCities()
        {
            var result = _repoCity.GetAll();
            return new JsonResult(result);
        }
        [HttpGet("{id}")]
        public JsonResult DeleteContact([FromRoute] int id)
        {
            Contact contact = new Contact();
            contact.Id = id;
            _repoContact.Delete(contact);
            return Json("Success");
        }
        [HttpPost]
        public JsonResult UnitOfWorkExample([FromBody] Contact contact)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork(new ContactdbContext());
                //unitOfWork.CityRepository.Add(contact);
                unitOfWork.ContactRepository.Add(contact);
                unitOfWork.CityRepository.Add(new City
                {
                    CityName = "mexico city",
                    Description = "in mexico"
                });
                unitOfWork.SaveChanges();
                return Json("Success");
            }
            catch(Exception ex)
            {
                return Json("false");
            }
        }
    }

}