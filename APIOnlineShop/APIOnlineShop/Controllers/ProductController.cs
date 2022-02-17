using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using LouigisSP.BO;
using LouigisSP.SL;
using APIOnlineShop.filters;
using System.Web.Http.Cors;
using LougisSP.BO.Exceptions;

namespace APIOnlineShop.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        // GET: api/Product
        // Allow CORS for all origins. (Caution!)
      
        public IHttpActionResult Get()
        {

            IEnumerable<Product> listProducts = null;
            ProductOperations productOperations = new ProductOperations();
            listProducts = productOperations.GetAllProducts();
            return Ok(listProducts);
        }

        // GET: api/Product/5°
        public IHttpActionResult Get(int id)
        {
            ProductOperations productOperations = new ProductOperations();
            Product product = productOperations.GetProductById(id);
            return Ok(product);
        }

        public IHttpActionResult Get(string pattern)
        {
            ProductOperations productOperations = new ProductOperations();
            IEnumerable<Product> listProducts = productOperations.GetProductsThatMatch(pattern);
            return Ok(listProducts);
        }

        public IHttpActionResult GetByCategory(string category)
        {
           throw new NotImplementedException();
        }


        [Authorize(Roles = "Employee")]
        public IHttpActionResult Post(Product product)
        {
            ProductOperations productOperations = new ProductOperations();
            productOperations.InsertProduct(product);
            return Ok("product added successfully");
        }

        [Authorize(Roles = "Employee")]
        public IHttpActionResult Put(int id, Product product)
        {
            if (product is null)
                throw new ArgumentNullException("Product can´t be null");
            if (product.id != id)
                throw new InvalidDataException("Id's does not match");

            ProductOperations productOperations = new ProductOperations();

            productOperations.EditProduct(id, product);
            return Ok("product eddited successfully");
        }



    }
}
