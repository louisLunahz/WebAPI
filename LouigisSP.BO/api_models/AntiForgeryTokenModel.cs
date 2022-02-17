using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LougisSP.BO.api_models
{
    public class AntiForgeryTokenModel
    {
        public string AntiForgeryToken { get; set; }

        public AntiForgeryTokenModel(string antiForgeryToken)
        {
            AntiForgeryToken = antiForgeryToken;
        }

        public AntiForgeryTokenModel()
        {
        }
    }
}