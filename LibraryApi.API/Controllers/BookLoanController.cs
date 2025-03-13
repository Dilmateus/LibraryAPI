using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.API.Controllers
{
    public class BookLoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
