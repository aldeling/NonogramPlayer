using Microsoft.AspNetCore.Mvc;
using Nonogram.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Nonogram.Controllers
{
  public class HomeController : Controller
  {
    private readonly NonogramContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public HomeController(UserManager<ApplicationUser> userManager, NonogramContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [HttpGet("/")]
    public async Task<ActionResult> Index()
    {
      Player[] players = _db.Players.ToArray();
      Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      model.Add("players", players);
      string userId = this._userManager.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      if (currentUser != null)
      {
        Nonogram[] nonograms = _db.Nonograms
          .Where(entery => entery.User.Id == currentUser.Id)
          .ToArray();
        model.Add("nonograms", nonograms);
      }
      return View(model);
    }
  }
}