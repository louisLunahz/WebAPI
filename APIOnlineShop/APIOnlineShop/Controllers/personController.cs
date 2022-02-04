
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using APIOnlineShop.models;
using APIOnlineShop.Security;
using APIOnlineShop.utilities;
using LougisSP.BO;
using LouigisSP.BO;
using LouigisSP.SL;
using LouigisSP.SL.Exceptions;

namespace APIOnlineShop.Controllers
{
    [AllowAnonymous]

    public class personController : ApiController
    {

   

        [HttpPost]
        [Route("api/person/authenticate")]
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            string email = login.email;
            string password = login.password;
            if (login == null)
                return BadRequest("credentials can´t be empty");

            

            //call the service layer, which will search the customer with that same email and password
            //and will return a customer which i then will return as a httpActionResult
            Authenticator authenticator = new Authenticator();
            try
            {
                Person person = authenticator.GetPerson(Tuple.Create(email, password));
                string rolename = null;
                if (person.Role == 1)
                    rolename = "Customer";
                if (person.Role == 2)
                    rolename = "Employee";
                int id = person.Id;
                var token = TokenGenerator.GenerateTokenJwt(login.email, rolename, id);
                person.Pass = null;
                Response response = new Response(token, person);
                return Ok(response);
            }
            catch (UserNotFoundException ex)
            {
                return Content(HttpStatusCode.Unauthorized, "Invlaid Credentials ");

            }




        }



        [HttpPost]
        [Route("api/person/register")]
        public IHttpActionResult Register(Person obj_person)
        {
            if (obj_person == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest("Data does not satisfy the requirements to register a new user");

            Authenticator authenticator = new Authenticator();
           Person person= authenticator.GetPersonByEmail(obj_person.Email);
            if (person != null)
                return BadRequest("That email is already in use");
            try
            {
                authenticator.InsertPerson(obj_person);
                return Ok("ok");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }



        [HttpGet]
        [Route("api/person/getInfo")]
        public IHttpActionResult GetInfo()
        {
           


            Authenticator authenticator = new Authenticator();
            try
            {
                Validator validator = new Validator();
                int id_currentPerson = validator.getIdCurrentPerson();
                Person person = authenticator.GetPersonById(id_currentPerson);
                return Ok(person);

            }catch (NoUserLoggedInException e)
            {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }


        [HttpPut]
        [Route("api/person/editInfo")]
        public IHttpActionResult EditInfo(int id, Person person)
        {
            //check if the current user id is the same as the one requested
            int id_currentPerson = -1;
            try
            {
                Validator validator = new Validator();
                 id_currentPerson = validator.getIdCurrentPerson();
            }
            catch (NoUserLoggedInException e)
            {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }

            if (id_currentPerson != id)
                return BadRequest("You are not allowed to edit that information");

            if (id != person.Id)
                return BadRequest("Id 's must match");

            if (!ModelState.IsValid)
                return BadRequest("Data does not satisfy the requirements to edit the user");


            Authenticator authenticator = new Authenticator();
            try
            {
                Person personModified = authenticator.EditPersonUsingId(id, person);
                return Ok(personModified);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }



        [HttpGet]
        [Route("api/person/getFavourites")]
        [Authorize]
        public IHttpActionResult GetFavourites()
        {

            int id_currentPerson = -1;
            Authenticator authenticator = new Authenticator();
            try
            {
                Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
                IEnumerable<Product> listProducts;
                listProducts = authenticator.GetFavourites(id_currentPerson);
                return Ok(listProducts);

            }
            catch (NoUserLoggedInException ex)
            {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }



        [HttpPost]
        [Route("api/person/addFavourite")]
        [Authorize]
        public IHttpActionResult addFavourite([FromBody]int id_product)
        {
            int id_currentPerson=-1;

            ProductOperations productOperations = new ProductOperations();
            try
            {
                Product productInProducts = productOperations.GetProductById(id_product);
            }
            catch (ProductNotFoundException e)
            {
                return BadRequest("Product with that id could not be found");
            }



            try
            {
                Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
                Authenticator authenticator = new Authenticator();

                IEnumerable<Product> listProducts = authenticator.GetFavourites(id_currentPerson);
                if (listProducts == null || listProducts.Count() == 0)
                {
                    authenticator.addFavourite(id_currentPerson, id_product);
                    return Ok("Product added succesfully");
                }
                Product producInFav = listProducts.Where(product => product.id == id_product).FirstOrDefault();
                if (producInFav != null)
                    return Ok("Product already in favorites");

                authenticator.addFavourite(id_currentPerson, id_product);
                return Ok("Product added succesfully");



            }
            catch (NoUserLoggedInException e)
            {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }


        [HttpDelete]
        [Route("api/person/deleteProductFromFavourites")]
        [Authorize]
        public IHttpActionResult deleteProductFromFavourites( int id_product)
        {
            int id_currentPerson=-1;

            try
            {
                Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
                Authenticator authenticator = new Authenticator();

                IEnumerable<Product> listProducts = authenticator.GetFavourites(id_currentPerson);
                if (listProducts == null || listProducts.Count() == 0)
                    return Ok("the customer still does not have any favourites");
                
                Product productInList = (Product)listProducts.Where(product => product.id == id_product).FirstOrDefault();
                if (productInList is null)
                    return Ok("Product is not in the favourites");

                authenticator.delFavourite(id_currentPerson, id_product);
                return Ok("Deleted succesfully");



            }
            catch (NoUserLoggedInException e)
            {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }



        }




        [HttpGet]
        [Route("api/person/getAddressCurrentUser")]
        [Authorize]
        public IHttpActionResult getAddressCurrentUser()
        {
            int id_currentPerson = -1;
            try
            {
                Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
            }
            catch (NoUserLoggedInException ex)
            {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }

            try
            {
                Authenticator obj_authenticator = new Authenticator();
                Address address = obj_authenticator.getUseraddress(id_currentPerson);
                return Ok(address);
            }catch (AddressNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

          





        }


       [HttpPost]
        [Route("api/person/makePayment")]
        [Authorize]
        public IHttpActionResult makePayment(Card card)
        {
            int id_currentPerson = -1;
            if (card is null)
                return BadRequest("card information can not be null");
          
            try
            {
                Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
            }
            catch (NoUserLoggedInException ex) {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }


            ShoppingCartOperations obj_shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> personCarts = obj_shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTableWithAtLeastOneItem(id_currentPerson);
            if (personCarts.Count() < 1)
                return BadRequest("There is no cart to be payed");


            if (!pay()) {
                return BadRequest("could not process the payment");
            }
            card.IdPerson=id_currentPerson;

            CardOperations objCardOperations = new CardOperations();
            try
            {
                objCardOperations.saveCard(card);
                OrderOperations obj_OrderOperations = new OrderOperations();
                obj_OrderOperations.insertOrder(personCarts.ElementAt(0).Id, id_currentPerson);

                return Ok();


            }
            catch (Exception ex) {
                return BadRequest("something went wrong while saving the card");
            }
        }





        private bool pay()
        {
            return true;
        }





    }
}
