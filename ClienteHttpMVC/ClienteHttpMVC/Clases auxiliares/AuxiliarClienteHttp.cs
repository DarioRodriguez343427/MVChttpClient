using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;

namespace ClienteHttpMVC.Clases_auxiliares
{
    public class AuxiliarClienteHttp
    {

        public static HttpResponseMessage EnviarSolicitud(string url, string verbo, object obj=null, string token=null)
        {
            HttpClient cliente = new HttpClient();

            if (!string.IsNullOrEmpty(token))
            {
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
            
            Task<HttpResponseMessage> tarea = null;

            if (verbo == "GET")
            {
                tarea = cliente.GetAsync(url);
            }
            else if (verbo == "POST")
            {
                tarea = cliente.PostAsJsonAsync(url, obj);
            }
            else if (verbo == "PUT")
            {
                tarea = cliente.PutAsJsonAsync(url, obj);
            }
            else if (verbo == "DELETE")
            {
                tarea = cliente.DeleteAsync(url);
            }

            tarea.Wait();
            return tarea.Result;            
        }

        public static string ObtenerBody(HttpResponseMessage respuesta)
        {
            HttpContent contenido = respuesta.Content;

            Task<string> tarea2 = contenido.ReadAsStringAsync();
            tarea2.Wait();

            return tarea2.Result;
        }
    }
}
