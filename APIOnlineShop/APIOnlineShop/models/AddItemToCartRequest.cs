using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIOnlineShop.models
{
    public class AddItemToCartRequest
    {
        [Required(ErrorMessage = "quantity is required")]
        public int quantity { get; set; }
        [Required(ErrorMessage = "id product is required")]
        public int id_product { get; set; }
    }
}