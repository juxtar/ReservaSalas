using ReservaSalas.Excepciones;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace WebApi.Filters
{
    public class YaExistenteExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is YaExistenteException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.Conflict)
                {
                    Content = new ObjectContent<YaExistenteException>(
                        actionExecutedContext.Exception as YaExistenteException,
                        new JsonMediaTypeFormatter()
                    )
                };
            }
        }
    }
}