using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public class Item
    {
        public int id{ get; set; }
        public int quantity { get; set; }
        public int id_product { get; set; }
        public int id_shoppingCart { get; set; }

        public float individual_total { get; set; }

        public Item(int id, int quantity, int id_product, int id_shoppingCart)
        {
            this.id = id;
            this.quantity = quantity;
            this.id_product = id_product;
            this.id_shoppingCart = id_shoppingCart;
            this.individual_total = individual_total;
        }

        public Item()
        {
        }
    }
}
