using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
    public class MstUserController : Controller
    {
        public IActionResult index()
        {
            return View();
        }

    }
}
