using ReservaSalas.Excepciones;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class AnulacionInvalidaExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is AnulacionInvalidaException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new ObjectContent<AnulacionInvalidaException>(
                        actionExecutedContext.Exception as AnulacionInvalidaException,
                        new JsonMediaTypeFormatter()
                    )
                };
            }
        }
    }
}