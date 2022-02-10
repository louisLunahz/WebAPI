using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.BO
{
    public class Card
    {
        public Card()
        {
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public string OwnerName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Cv { get; set; }
        public int IdPerson { get; set; }

        public Card(int id, string number, string ownerName, int month, int year, int cv, int idPerson)
        {
            Id = id;
            Number = number;
            OwnerName = ownerName;
            this.Month = month;
            this.Year = year;
            this.Cv = cv;
            IdPerson = idPerson;
        }
    }
}
