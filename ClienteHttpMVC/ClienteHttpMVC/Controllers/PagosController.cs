using CasosUso.DTOs;
using ClienteHttpMVC.Clases_auxiliares;
using ClienteHttpMVC.DTO;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentacion.Controllers;

namespace ClienteHttpMVC.Controllers
{
    public class PagosController : BaseController
    {
        public string URLApiPagos { get; set; }
        public string URLApiTipoGastos { get; set; }

        public PagosController(IConfiguration config)
        {
            URLApiPagos = config.GetValue<string>("URLApiPagos");
            URLApiTipoGastos = config.GetValue<string>("URLApiTipoGastos");
        }

        [HttpGet]
        public IActionResult SeleccionarTipoPago()
        {
            if(ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        /*----------------- PAGO UNICO------------------------*/
        [HttpGet]
        public IActionResult CreatePagoUnico()
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }

            IEnumerable<TipoGastoDTO> listado = null;
            try
            {
                string token = HttpContext.Session.GetString("token");
                var resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(resultado);

                if (resultado.IsSuccessStatusCode)
                {
                    listado = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(body);
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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePagoUnico(PagoUnicoDTO dto)
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (dto == null) return BadRequest("no hay datos para procesar");

            try
            {
                dto.IdUsuario = int.Parse(HttpContext.Session.GetString("id"));

                string token = HttpContext.Session.GetString("token");
                var resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApiPagos + "/altaPagoUnico", "POST", dto, token);

                if (resultado.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Usuarios");
                }
                else
                {
                    string body = AuxiliarClienteHttp.ObtenerBody(resultado);
                    ViewBag.Mensaje = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Ha ocurrido un error inesperado";
            }

            return View(dto);
        }
        /* ------------------------FIN PAGO UNICO ------------------------------*/

        /*----------------- INICIO PAGO RECURRENTE------------------------*/
        [HttpGet]
        public IActionResult CreatePagoRecurrente()
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }

            IEnumerable<TipoGastoDTO> listado = null;
            try
            {
                string token = HttpContext.Session.GetString("token");
                var resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(resultado);

                if (resultado.IsSuccessStatusCode)
                {
                    listado = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(body);
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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePagoRecurrente(PagoRecurrenteDTO dto)
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (dto == null) return BadRequest("no hay datos para procesar");

            try
            {
                dto.IdUsuario = int.Parse(HttpContext.Session.GetString("id"));

                string token = HttpContext.Session.GetString("token");
                var resultado = AuxiliarClienteHttp.EnviarSolicitud(URLApiPagos + "/altaPagoRecurrente", "POST", dto, token);

                if (resultado.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Usuarios");
                }
                else
                {
                    string body = AuxiliarClienteHttp.ObtenerBody(resultado);
                    ViewBag.Mensaje = body;
                }
            }
            catch (Exception)
            {
                ViewBag.Mensaje = "Ha ocurrido un error inesperado";
            }

            return View(dto);
        }
        /* ------------------------FIN PAGO RECURRENTE ------------------------------*/



        [HttpGet]
        public IActionResult listadoPagos()
        {
            if (ObtenerRol() == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else if (ObtenerRol() != "Gerente" && ObtenerRol() != "Empleado")
            {
                ViewBag.Rol = "Su rol no permite realizar esta operación";
            }

            IEnumerable<PagosUsuarioDTO> lista = new List<PagosUsuarioDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                int idUsuario = int.Parse(HttpContext.Session.GetString("id"));

                var respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiPagos + "/" + idUsuario, "GET", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    lista = JsonConvert.DeserializeObject<List<PagosUsuarioDTO>>(body);
                }
                else
                {
                    ViewBag.Mensaje = body;
                }
            }
            catch
            {
                ViewBag.Mensaje = "ocurrio un error";
            }
            return View(lista);
        }
    }
}
