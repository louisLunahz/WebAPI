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
    public class FavouritesController : ApiController
    {
        [HttpGet]
        [Route("api/favourites/getFavourites")]
        [Authorize]
        public IHttpActionResult GetFavourites()
        {

            int id_currentPerson = -1;
            FavouritesOperations obj_favouritesOperatons = new FavouritesOperations();

            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();
            IEnumerable<Product> listProducts;
            listProducts = obj_favouritesOperatons.GetFavourites(id_currentPerson);
            return Ok(listProducts);
        }



        [HttpPost]
        [Route("api/favourites/addFavourite")]
        [Authorize]
        public IHttpActionResult addFavourite([FromBody] int id_product)
        {
            int id_currentPerson = -1;
            ProductOperations productOperations = new ProductOperations();
            Product productInProducts = productOperations.GetProductById(id_product);
            Validator validator = new Validator();
            id_currentPerson = validator.getIdCurrentPerson();
            FavouritesOperations obj_favouritesOperations = new FavouritesOperations();

            IEnumerable<Product> listProducts = obj_favouritesOperations.GetFavourites(id_currentPerson);
            if (listProducts == null || listProducts.Count() == 0)
            {
                obj_favouritesOperations.addFavourite(id_currentPerson, id_product);
                return Ok("Product added succesfully");
            }
            Product producInFav = listProducts.Where(product => product.id == id_product).FirstOrDefault();
            if (producInFav != null)
                return Ok("Product already in favorites");

            obj_favouritesOperations.addFavourite(id_currentPerson, id_product);
            return Ok("Product added succesfully");

        }


        [HttpDelete]
        [Route("api/favourites/deleteProductFromFavourites")]
        [Authorize]
        public IHttpActionResult deleteProductFromFavourites(int id_product)
        {
            int id_currentPerson = -1;

               Validator validator = new Validator();
                id_currentPerson = validator.getIdCurrentPerson();
                FavouritesOperations obj_favouritesOperations = new FavouritesOperations();

                IEnumerable<Product> listProducts = obj_favouritesOperations.GetFavourites(id_currentPerson);
                if (listProducts == null || listProducts.Count() == 0)
                    return Ok("the customer still does not have any favourites");

                Product productInList = (Product)listProducts.Where(product => product.id == id_product).FirstOrDefault();
                if (productInList is null)
                    return Ok("Product is not in the favourites");

                obj_favouritesOperations.delFavourite(id_currentPerson, id_product);
                return Ok("Deleted succesfully");

        }


    }
}
