using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LouigisSP.BO
{
    public class Person : Fileable
    {
       


        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddressAttribute ]

        public string Email { get; set; }   

        public string Pass { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Role { get; set; }

        protected Person(int id, string firstName, string lastName, string phoneNumber, string email, string pass, DateTime dateOfBirth, int role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Pass = pass;
            DateOfBirth = dateOfBirth;
            Role = role;
        }

        public Person() { 
        
        }


      
        public int CompareTo(object obj)
        {
            Person obj_user = (Person)obj;
            return this.Email.CompareTo(obj_user.Email);
        }


        public override string ToString()
        {
            return string.Format("{0,15} {1,15} {2,15} {3,15}", Id, FirstName, LastName, Email);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

       

    }
}
