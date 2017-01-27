using ReservaSalas.Excepciones;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class EncuestaInvalidaExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is EncuestaInvalidaException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent<EncuestaInvalidaException>(
                        actionExecutedContext.Exception as EncuestaInvalidaException,
                        new JsonMediaTypeFormatter()
                    )
                };
            }
        }
    }
}