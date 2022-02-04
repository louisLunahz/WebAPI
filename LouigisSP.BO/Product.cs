using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public class Product: Fileable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public string  model { get; set; }
        public string  color { get; set; }
        public float price { get; set; }
        public int stock { get; set; }
        public string extra_info { get; set; }
        public string barcode { get; set; }
        public string src_image { get; set; }







       

        public override string ToString()
        {
            return "Id: "+id+"    Name: " + name + "    Brand:" + brand + "    Model:" + model + "    Color:" + color + "    Price:" + price + "    Stock:" + stock+ "   Info: "+extra_info; 
        }

        public void showDetails() {
            Console.Write("Id: "+id+" ");
            Console.Write("Name: "+name + " ");
            Console.Write("Brand: "+brand + " ");
            Console.Write("Model: "+model + " ");
            Console.Write("Color: "+color + " ");
            Console.Write("Price: "+price + " ");
            Console.Write("Description: "+ extra_info);

        }

       
    }
}
