using Bookist.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookist.Web.Controllers;

public class HomeController : Controller
{
    public ViewResult Index()
    {
        var vm = new BookVM
        {
            Title = "CLR via C# (4th Edition)",
            Cover = "https://img.geekgist.com/Foc1d-NbacAQ6D1WSQ_3UndhaOuR-w2h3",
            Author = "Jeffrey Richter",
            PubDate = new DateOnly(2012, 10, 1),
            Description = "Dig deep and master the intricacies of the CLR, C#, and .NET development. You’ll gain pragmatic insights for building robust, reliable, and responsive apps and components.",
            Format = "PDF",
            FetchUrl = "https://url19.ctfile.com/f/15677019-228693113-e89578",
            FetchCode = "bookist"
        };
        return View(vm);
    }
}
