using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LougisSP.BO
{
    public class Address
    {
        public Address()
        {
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int ZipCode { get; set; }
        public string State { get; set; }
        public string City{ get; set; }
        public string Country { get; set; }
        public int IdPerson { get; set; }

        public Address(int id, string street, int number, int zipCode, string state, string city, string country, int idPerson)
        {
            Id = id;
            Street = street;
            Number = number;
            ZipCode = zipCode;
            State = state;
            City = city;
            Country = country;
            IdPerson = idPerson;
        }

    }
}
