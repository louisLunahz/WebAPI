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
using APIOnlineShop.models;

namespace APIOnlineShop.Controllers
{
    
    public class ProductController : ApiController
    {
        // GET: api/Product
        public IHttpActionResult Get()
        {

            IEnumerable<Product> listProducts = null;
            ProductOperations productOperations = new ProductOperations();
            //return all the products
            try
            {

                listProducts = productOperations.GetAllProducts();
                    return Ok(listProducts);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Product/5
        public IHttpActionResult Get(int id)
        {
            ProductOperations productOperations = new ProductOperations();
            try
            {
                Product product = productOperations.GetProductById(id);
               
                return Ok(product);
            }
            catch (Exception e)
            {
                return NotFound();
            }

        }


        [Authorize(Roles = "Employee")]
        public IHttpActionResult Post(Product product)
        {
            ProductOperations productOperations = new ProductOperations();
            try
            {
                productOperations.InsertProduct(product);
                return Ok("product added successfully");
            }catch (Exception e)
            {
                return BadRequest("could not insert the product"+ e.Message);           
            }
        }

        [Authorize(Roles = "Employee")]
        public IHttpActionResult Put(int id, Product product)
        {
            if (id != product.id)
                return BadRequest("Product's id must be the same as the one you are trying to update");

            ProductOperations productOperations = new ProductOperations();
            try
            {
                productOperations.EditProduct(id, product);
                return Ok("product eddited successfully");
            }
            catch (Exception e)
            {
                return BadRequest("could not insert the product" + e.Message);
            }
        }



    }
}
