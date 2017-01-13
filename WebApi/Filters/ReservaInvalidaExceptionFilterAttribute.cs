using ReservaSalas.Excepciones;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class ReservaInvalidaExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ReservaInvalidaException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new ObjectContent<ReservaInvalidaException>(
                        actionExecutedContext.Exception as ReservaInvalidaException,
                        new JsonMediaTypeFormatter()
                    )
                };
            }
        }
    }
}