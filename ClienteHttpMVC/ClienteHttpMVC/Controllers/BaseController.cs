using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentacion.Controllers
{
    public class BaseController : Controller
    {
        protected string ObtenerRol()
        {
            return HttpContext.Session.GetString("rol");
        }


    }
}
