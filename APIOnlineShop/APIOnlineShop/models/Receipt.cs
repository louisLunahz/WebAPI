using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIOnlineShop.models
{
    public class Receipt
    {
        public int OrderDate { get; set; }
        public int OrderNumber { get; set; }
        public List<Item> Items { get; set; }
        public float Total { get; set; }
    }
}