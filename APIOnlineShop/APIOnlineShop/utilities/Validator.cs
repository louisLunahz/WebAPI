using APIOnlineShop.BO.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;

namespace APIOnlineShop.Exceptions
{
    public class Validator
    {
      

        public int getIdCurrentPerson()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            if (claims == null || claims.Count() < 3)
                throw new InvalidCredentialsException("Could not get the session");

            int id_currentPerson = int.Parse(claims.ElementAt(2).Value);
            return id_currentPerson;

        }



    }
}