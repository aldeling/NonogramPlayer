using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NonogramPuzzle.Models;

namespace NonogramPuzzle.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}




// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;

// using Puzzle.Models;

// namespace Puzzle.Controllers
// {
//   public class HomeController : Controller
//   {
//     private readonly NonogramContext _db;
//     public HomeController(NonogramContext db)
//     {
//       _db = db;
//     }

//     [HttpGet("/")]
//     public ActionResult Index()
//     {
//       Player[] players = _db.Players.ToArray();
//       Nonogram[] nonogram = _db.Nonograms.ToArray();
//       Dictionary<string, object[]> model = new Dictionary<string, object[]>();
//       model.Add("players", players);
//       model.Add("nonograms", nonogram);
//       return View(model);
//     }
//   }
// }