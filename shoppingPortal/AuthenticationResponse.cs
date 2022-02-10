using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace shoppingPortal
{
    public partial class AuthenticationResponse
    {
        public string Token { get; set; }
        public Person Person { get; set; }
    }

    public partial class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public object Pass { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public long Role { get; set; }
    }

}









