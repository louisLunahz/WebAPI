using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace APIOnlineShop.models
{
    public class Response
    {
        public string token { get; set; }

        public Person person { get; set; }

        public Response(string token, Person person)
        {
            this.token = token;
            this.person = person;
        }
    }
}