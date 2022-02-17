using LouigisSP.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LougisSP.BO.api_models
{
    public class AuthResponse
    {
        public string token { get; set; }

        public Person person { get; set; }

        public AuthResponse(string token, Person person)
        {
            this.token = token;
            this.person = person;
        }
    }
}