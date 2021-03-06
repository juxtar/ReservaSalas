﻿using ReservaSalas.Excepciones;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class SalaInvalidaExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is SalaInvalidaException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent<SalaInvalidaException>(
                        actionExecutedContext.Exception as SalaInvalidaException,
                        new JsonMediaTypeFormatter()
                    )
                };
            }
        }
    }
}