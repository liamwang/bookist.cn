using Microsoft.AspNetCore.Mvc;

namespace GeekGist.Web.Controllers;

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
