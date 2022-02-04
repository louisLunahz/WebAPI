using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using LouigisSP.BO;
using LouigisSP.BO;
using LouigisSP.SL;
using APIOnlineShop.models;
using APIOnlineShop.utilities;

namespace APIOnlineShop.Controllers
{
    public class CartController : ApiController
    {

        [HttpPost]
        [Route("api/cart/AddProductToCart")]
        [Authorize]
        public IHttpActionResult AddProductToCart(AddItemToCartRequest addproductTocartRequest)
        {

            if (addproductTocartRequest == null)
                return BadRequest("request was expected");

            int id_currentPerson = -1;
            try
            {
                Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
            }
            catch(NoUserLoggedInException ex) {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }
          
            ProductOperations productOperations = new ProductOperations();
            Product obj_product = productOperations.GetProductById(addproductTocartRequest.id_product);

            if (obj_product is null)
                return BadRequest("Product does not exist or is not avaliable");
            if (obj_product.stock < addproductTocartRequest.quantity)
                return BadRequest("Not enoug stock");
            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> carts = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
            if (carts.Count() <= 0)
            {
                bool wasCreated = shoppingCartOperations.CreateCartForPerson(id_currentPerson);
                if (!wasCreated)
                {
                    return BadRequest("Error creating a cart for the customer");
                }
                IEnumerable<Cart> carts2 = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
                if (carts2.Count() == 1)
                {
                    int idCart = carts2.ElementAt(0).Id;
                    //add item to items table
                    AddItemToCart(obj_product, idCart, addproductTocartRequest.quantity);
                    return Ok();
                }
                else
                {
                    return BadRequest("Unable to add item to the cart");
                }
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
        public IHttpActionResult deleteProductFromCart( int id_item)
        {
            int id_currentPerson = -1;
            try {
                Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
            }
            catch (NoUserLoggedInException ex)
            {
                return Content(HttpStatusCode.Unauthorized, "You are not allowed to perform that action ");
            }
         


            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> carts = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
            if (carts.Count() < 1)
                return BadRequest("Customer does not have a cart");
            int id_cart = carts.ElementAt(0).Id;
            try
            {
                shoppingCartOperations.DeleteItemFromCart(id_item, id_cart);
                return Ok("product deleted succesfully");

            }
            catch (Exception e)
            {
                return BadRequest("Error while deleting the product");
            }

        }

        [HttpGet]
        [Route("api/cart/GetAllItemsInCart")]
        [Authorize]
        public IHttpActionResult GetAllItemsInCart()
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

            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            IEnumerable<Cart> carts = shoppingCartOperations.GetAllCartsRelatedToOnePersonNotInTheOrdersTable(id_currentPerson);
            if (carts.Count() >= 1) {
               IEnumerable<Item> Listitems= shoppingCartOperations.GetAllItemsInCart(carts.ElementAt(0).Id);
                return Ok(Listitems);
            }
            else
            {
                return Ok("no items in card");
            }
        }

       



        private bool AddItemToCart(Product product, int id_cart, int quantity)
        {

            ShoppingCartOperations shoppingCartOperations = new ShoppingCartOperations();
            try
            {
                shoppingCartOperations.InsertItem(product, id_cart, quantity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }





    }
}
