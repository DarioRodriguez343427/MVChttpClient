using ClienteHttpMVC.Clases_auxiliares;
using ClienteHttpMVC.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentacion.Controllers;

namespace ClienteHttpMVC.Controllers
{
    public class EquiposController : BaseController
    {
        private string URLApiEquipos { get; set; }

        public EquiposController(IConfiguration config, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                URLApiEquipos = config.GetValue<string>("URLApiEquipos");
            }

            if (env.IsProduction())
            {
                URLApiEquipos = config.GetValue<string>("URLApiEquiposAZURE");
            }
        }


        [HttpGet]
        public IActionResult ListarEquiposSegunMonto(double? monto)
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (ObtenerRol() != "Gerente") ViewBag.Error = "Su rol no esta autorizado a realizar esta operacion";

            IEnumerable<EquipoDTO> lista = new List<EquipoDTO>();

            if (monto == null) return View(lista);

            try
            {
                var token = HttpContext.Session.GetString("token");
                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiEquipos + "/" + monto, "GET",null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    lista = JsonConvert.DeserializeObject<List<EquipoDTO>>(body);
                }
                else
                {
                    ViewBag.mensaje = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "ocurrio un error inesperado, intente nuevamente";
            }
            return View(lista);
        }
    }
}
