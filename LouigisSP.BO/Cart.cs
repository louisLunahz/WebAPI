using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public  class Cart
    {
        public int Id { get; set; }
        public int IdPerson { get; set; }

        public Cart(int id, int idPerson)
        {
            Id = id;
            IdPerson = idPerson;
        }

        public Cart()
        {
        }
    }
}
