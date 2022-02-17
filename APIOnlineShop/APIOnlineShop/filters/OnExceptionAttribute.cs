using LougisSP.BO.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace APIOnlineShop.filters
{
    public class OnExceptionAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //switch exceptions 
            HttpResponseMessage resp;

          
            if (context.Exception is ActionNotAllowedException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode = HttpStatusCode.Unauthorized
                };
                throw new HttpResponseException(resp);
            }
            if (context.Exception is DatabaseInsertionException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode= HttpStatusCode.NotFound
                };
                throw new HttpResponseException(resp);
            }
            if (context.Exception is ElementNotDeletedException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode= HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }
            if (context.Exception is ElementNotFoundException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode = HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }
            if (context.Exception is InvalidCredentialsException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode=HttpStatusCode.Unauthorized
                };
                throw new HttpResponseException(resp);
            }
            if (context.Exception is InvalidDataException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode= HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }
            if (context.Exception is NoValuesToInsertException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode = HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }
            if (context.Exception is ArgumentNullException)
            {
                resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception.Message),
                    StatusCode = HttpStatusCode.BadRequest
                };
                throw new HttpResponseException(resp);
            }









        }
    }
}