using CasosUso.DTOs;
using ClienteHttpMVC.Clases_auxiliares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Presentacion.Controllers
{
    public class BaseController : Controller
    {
        protected string ObtenerRol()
        {
            return HttpContext.Session.GetString("rol");
        }

        protected void CargarTiposDeGasto(string token, string url)
        {
            try
            {
                var res = AuxiliarClienteHttp.EnviarSolicitud(url, "GET", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(res);

                if (res.IsSuccessStatusCode)
                {
                    IEnumerable<TipoGastoDTO> listado = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(body);
                    ViewBag.TiposDeGasto = listado;
                }
                else
                {
                    ViewBag.Mensaje = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Ha ocurrido un error inesperado";
            }
        }
    }
}
