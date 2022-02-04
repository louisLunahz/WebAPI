using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public class Order
    {
        public Order()
        {
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public int IdCart { get; set; }
        public int IdPerson { get; set; }

        public Order(int id, DateTime orderDate, string status, int idCart, int idPerson)
        {
            Id = id;
            OrderDate = orderDate;
            Status = status;
            IdCart = idCart;
            IdPerson = idPerson;
        }
    }
}
