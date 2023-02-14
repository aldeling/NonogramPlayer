using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

using Puzzle.Models;

namespace Puzzle.Controllers
{
  public class HomeController : Controller
  {
    private readonly NonogramContext _db;
    public HomeController(NonogramContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      Player[] players = _db.Players.ToArray();
      Nonogram[] nonogram = _db.Nonograms.ToArray();
      Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      model.Add("players", players);
      model.Add("nonograms", nonogram);
      return View(model);
    }
  }
}