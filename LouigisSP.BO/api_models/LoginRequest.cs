using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LougisSP.BO.api_models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "email is required")]
        public string email { get; set; }

        [Required(ErrorMessage = "password is required")]
        [StringLength(8)]
        public string password { get; set; }
    }
}