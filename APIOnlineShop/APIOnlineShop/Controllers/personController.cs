
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using APIOnlineShop.models;
using APIOnlineShop.Security;
using APIOnlineShop.Exceptions;
using LouigisSP.BO;
using LouigisSP.SL;
using APIOnlineShop.BO.Exceptions;
using System.Net.Http.Headers;
using APIOnlineShop.filters;
using System.Web;
using System.Web.Helpers;
using APIOnlineShop.models.API_Models;
using System.Threading;
using System.Security.Principal;

namespace APIOnlineShop.Controllers
{
  

    public class personController : ApiController
    {



        [HttpPost]
        [Route("api/person/authenticate")]
      
        public HttpResponseMessage Authenticate(LoginRequest login)
        {

            if (login == null)
                throw new InvalidCredentialsException();
           
            PersonOperations authenticator = new PersonOperations();
            Person person = authenticator.GetPersonByEmailAndPassword(Tuple.Create(login.email, login.password));
            string rolename = null;
            if (person.Role == 1)
                rolename = "Customer";
            if (person.Role == 2)
                rolename = "Employee";
            int id = person.Id;

            string jwtToken = TokenGenerator.GenerateTokenJwt(person.Email, rolename, id);
            HttpCookie cookie = HttpContext.Current.Request.Cookies["XSRF-TOKEN"];

            string cookieToken;
            string formToken;
            AntiForgery.GetTokens(cookie == null ? "" : cookie.Value, out cookieToken, out formToken);
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            person.Pass = null;
            AuthResponse obj_authResponse = new AuthResponse(jwtToken,person, formToken);
            HttpResponseMessage response = Request.CreateResponse<AuthResponse>(HttpStatusCode.OK, obj_authResponse);


            if (!string.IsNullOrEmpty(cookieToken))
            {
                response.Headers.AddCookies(new[]
                {
                    new CookieHeaderValue("XSRF-TOKEN", cookieToken)
                     {
                        Expires = DateTimeOffset.Now.AddMinutes(10),
                        Path = "/; SameSite=None",
                        Secure = true

                     }
                });
            }

            return response;
        }



        [HttpPost]
        [Route("api/person/register")]
        public IHttpActionResult Register(Person obj_person)
        {
            if (obj_person == null)
                throw new ArgumentNullException();

            if (!ModelState.IsValid)
                throw new InvalidDataException("Data is not valid or does not match the model");

            PersonOperations obj_personOperations = new PersonOperations();
            Person person = obj_personOperations.GetPersonByEmail(obj_person.Email);
            if (person != null)
                throw new Exception("email already in use");

            obj_personOperations.InsertPerson(obj_person);
            return Ok("ok");
        }



        [HttpGet]
        [Route("api/person/getInfo")]
        public IHttpActionResult GetInfo()
        {
            PersonOperations authenticator = new PersonOperations();
            Validator validator = new Validator();
            int id_currentPerson = validator.getIdCurrentPerson();
            Person person = authenticator.GetPersonById(id_currentPerson);
            return Ok(person);
        }


        [HttpPut]
        [Route("api/person/editInfo")]
        public IHttpActionResult EditInfo(int id, Person person)
        {
            //check if the current user id is the same as the one requested
            int id_currentPerson = -1;
            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();


            if (id_currentPerson != id)
                throw new InvalidCredentialsException("The id given does not match with the session id");

            if (id != person.Id)
                throw new InvalidCredentialsException("The id must be the same as the one you are trying to update");


            if (!ModelState.IsValid)
                throw new InvalidDataException("Data is not valid or does not match with the model");


            PersonOperations authenticator = new PersonOperations();

            Person personModified = authenticator.EditPersonUsingId(id, person);
            return Ok(personModified);





        }




















    }
}
