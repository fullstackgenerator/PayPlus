using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PayPlus.Models;

namespace PayPlus.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    public JsonResult GetEvents()
    {
        var events = new List<object>
        {
            new { title = "Meeting", start = "2024-03-20", end = "2024-03-21" },
            new { title = "Conference", start = "2024-03-25" }
        };
        return Json(events);
    }
}