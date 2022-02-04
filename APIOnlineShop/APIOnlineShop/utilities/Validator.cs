using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;

namespace APIOnlineShop.utilities
{
    public class Validator
    {
        //checks the password to be strong, password must have at least 8 characters, must contain at least one capital letter
        public static bool VerifyPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) 
                throw new ArgumentNullException("password");
            string hasNumber = @"[0-9]+";
            string hasUpperChar = @"[A-Z]+";
            string hasMinimum8Chars = @".{8,}";
            if ((Regex.IsMatch(password, hasNumber) && Regex.IsMatch(password, hasUpperChar) && Regex.IsMatch(password, hasMinimum8Chars)))
                return true;
            throw new InvalidValueException("Passwords are not the same");
        }

        //checks the name to have more than 3 letters
        public static bool CheckName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("password");
            if (name.Length >= 3)
                return true;
            throw new InvalidValueException("Password bad formated");
        }

        //checks the email to be well formated 
        public static bool CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) 
                throw new ArgumentNullException("email");
            if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) 
                return true;
            throw new InvalidValueException("Email bad formated");
        }

       //check the date of birth to not be null, and not to be older than 100 years
        public static bool CheckDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth == null)
                throw new ArgumentNullException("date of birth");
            TimeSpan maxAgeAccepted = new TimeSpan(36500, 0, 0, 0); //Max age accepted is 36500 days
            TimeSpan timeDifference = DateTime.Now.Subtract(dateOfBirth);
            if (timeDifference <= maxAgeAccepted)
                return true;
            throw new InvalidValueException();
        }

        //checks for a phone number to have 10 digits and accepts the following formats
        //2222222222, 222-222-2222, 
        public static bool CheckPhoneNumber(string phone)
        {
            if (string.IsNullOrEmpty(phone)) 
                throw new ArgumentNullException(phone);
            string pattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
            if (Regex.IsMatch(phone, pattern)) 
                return true;
            throw new InvalidValueException();
        }

        public static bool CheckAddress(string address)
        {
            if (string.IsNullOrEmpty(address)) 
                throw new ArgumentNullException("address");
            //if more conditions shuld be satisfied, they should be put here
            return true;
        }

        internal static bool CheckDate(DateTime date)
        {
           if(date == null) throw new ArgumentNullException("date");
            //check if date is well formated 
            
            
            return true;
           
        }

        public int getIdCurrentPerson()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            if (claims == null || claims.Count() < 3)
                throw new NoUserLoggedInException("Could not get the current user claims");

            int id_currentPerson = int.Parse(claims.ElementAt(2).Value);
            return id_currentPerson;

        }



    }
}