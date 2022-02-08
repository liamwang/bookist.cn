using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    public ViewResult Sponsor()
    {
        return View();
    }
}
