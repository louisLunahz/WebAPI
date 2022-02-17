using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using LouigisSP.BO;
using LouigisSP.SL;
using APIOnlineShop.Exceptions;
using APIOnlineShop.filters;
using System.Threading;
using LougisSP.BO.api_models;
using LougisSP.BO.Exceptions;

namespace APIOnlineShop.Controllers
{
    [ValidateAntiForgeryTokenFilter]
    public class CartController : ApiController
    {

        [HttpPost]
        [Route("api/cart/AddProductToCart")]
        [Authorize]
      
        public IHttpActionResult AddProductToCart(AddItemToCartRequest addproductTocartRequest)
        {

            if (addproductTocartRequest == null)
                throw new ArgumentNullException("No argument was given");
            int id_currentPerson = -1;
            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();





            ProductOperations productOperations = new ProductOperations();
            Product obj_product = productOperations.GetProductById(addproductTocartRequest.id_product);

            if (obj_product is null)
                throw new ElementNotFoundException();
            if (obj_product.stock < addproductTocartRequest.quantity)
                throw new InvalidDataException("Stock is not enough");
            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> carts = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
            if (carts.Count() <= 0)
            {
                shoppingCartOperations.CreateCartForPerson(id_currentPerson);

                IEnumerable<Cart> carts2 = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
                if (carts2.Count() == 1)
                {
                    int idCart = carts2.ElementAt(0).Id;
                    //add item to items table
                    AddItemToCart(obj_product, idCart, addproductTocartRequest.quantity);
                    return Ok();
                }

                throw new Exception("Could not create a cart for the customer");

            }
            else
            {
                int idCart = carts.ElementAt(0).Id;
                AddItemToCart(obj_product, idCart, addproductTocartRequest.quantity);
                return Ok();
            }






        }


        [HttpDelete]
        [Route("api/cart/deleteProductFromCart")]
        [Authorize]
        public IHttpActionResult deleteProductFromCart(int id_item)
        {
            int id_currentPerson = -1;
            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();
            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> carts = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
            if (carts.Count() < 1)
                throw new ElementNotDeletedException("Item could not be deleted because the user still does not have a cart with products in it");
            int id_cart = carts.ElementAt(0).Id;
            shoppingCartOperations.DeleteItemFromCart(id_item, id_cart);
            return Ok("product deleted succesfully");
        }

        [HttpGet]
        [Route("api/cart/GetAllItemsInCart")]
        [Authorize]
       

        public IHttpActionResult GetAllItemsInCart()
        {
           var identities= ClaimsPrincipal.Current.Identities.ToList();
            int id_currentPerson = -1;
            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();

            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> carts = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
            if (carts.Count() >= 1)
            {
                IEnumerable<Item> Listitems = shoppingCartOperations.GetAllItemsInCart(carts.ElementAt(0).Id);
                return Ok(Listitems);
            }
            else
                throw new ElementNotFoundException("No items were found for customer with id: "+ id_currentPerson);
            
        }





        private bool AddItemToCart(Product product, int id_cart, int quantity)
        {

            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            shoppingCartOperations.InsertItem(product, id_cart, quantity);
            return true;
        }


        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Headers.Add("Access-Control-Allow-Origin", "*");
            resp.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE");

            return resp;
        }


    }
}
