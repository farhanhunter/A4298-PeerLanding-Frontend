using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FE.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult index()
        {
            return View();
        }
    }
}
