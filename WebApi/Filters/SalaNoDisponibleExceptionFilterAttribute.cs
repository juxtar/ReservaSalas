using ReservaSalas.Excepciones;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class SalaNoDisponibleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is SalaNoDisponibleException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new ObjectContent<SalaNoDisponibleException>(
                        actionExecutedContext.Exception as SalaNoDisponibleException,
                        new JsonMediaTypeFormatter()
                    )
                };
            }
        }
    }
}