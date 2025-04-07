using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VehiclesTitles.Frontend.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
          Response = response;
          Error = error;
          HttpActionResponseMessage = httpResponseMessage;

        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpActionResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync(HttpResponseMessage httpResponseMessage)
        {
            if (!Error)
            {
                return null;
            }
            var statusCode = HttpActionResponseMessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "No se encontró el recurso.";
            }
            if (statusCode == HttpStatusCode.BadRequest)
            {
                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            if (statusCode == HttpStatusCode.Conflict)
            {
                return "Conflicto en la solicitud.";
            }
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tiene que estar logueado para ejecutar esta operación.";
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tiene permisos para hacer esta operación.";
            }
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                return "Error interno del servidor.";
            }
            else
            {
                var errorMessage = await HttpActionResponseMessage.Content.ReadAsStringAsync();
                return errorMessage;
            }

        }
    }
}