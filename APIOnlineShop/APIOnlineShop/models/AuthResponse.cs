using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace APIOnlineShop.models
{
    public class AuthResponse
    {
        public string token { get; set; }

        public Person person { get; set; }
        public string CSRFToken { get; set; }

        public AuthResponse(string token, Person person, string CSRFToken)
        {
            this.token = token;
            this.person = person;
            this.CSRFToken = CSRFToken;
        }
    }
}