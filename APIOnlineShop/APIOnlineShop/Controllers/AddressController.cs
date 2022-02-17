using APIOnlineShop.Exceptions;
using APIOnlineShop.filters;
using LouigisSP.BO;
using LouigisSP.SL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIOnlineShop.Controllers
{
    [ValidateAntiForgeryTokenFilter]
    public class AddressController : ApiController
    {

        [HttpGet]
        [Route("api/address/getAddressCurrentUser")]
        [Authorize]
        public IHttpActionResult getAddressCurrentUser()
        {
            int id_currentPerson = -1;
            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();
            PersonOperations obj_authenticator = new PersonOperations();
            Address address = obj_authenticator.getUseraddress(id_currentPerson);
            return Ok(address);
        }
    }
}
