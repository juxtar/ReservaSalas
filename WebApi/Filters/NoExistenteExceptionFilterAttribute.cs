using System.Web.Http.Filters;
using ReservaSalas.Excepciones;
using System.Net.Http;
using System.Net;
using System.Net.Http.Formatting;

namespace WebApi.Filters
{
    public class NoExistenteExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is NoExistenteException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new ObjectContent<NoExistenteException>(
                        actionExecutedContext.Exception as NoExistenteException,
                        new JsonMediaTypeFormatter()
                    )
                };
            }
        }
    }
}