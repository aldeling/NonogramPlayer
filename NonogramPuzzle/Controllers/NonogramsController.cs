using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

using NonogramPuzzle.Models;

namespace NonogramPuzzle.Controllers
{
  //[Authorize]
  public class NonogramsController : Controller
  {
    private readonly NonogramPuzzleContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public NonogramsController(UserManager<ApplicationUser> userManager, NonogramPuzzleContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);

      return View();
    }

    public ActionResult Create()
    {
      return View();
    }

    // [HttpPost]
    // public async Task<ActionResult> Create(Nonogram nonogram)
    // {
    //   if (!ModelState.IsVaild)
    //   {
    //     return View(nonogram);
    //   }
    //   else
    //   {
    //     string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    //     nonogram.User = currentUser;
    //     _db.Nonograms.Add(nonogram);
    //     _db.SaveChanges();
    //     return RedirectToAction("Index");
    //   }
    // }

    public ActionResult Details(int id)
    {
      Nonogram thisNonogram = _db.Nonograms
        .Include(nonogram => nonogram.JoinEntities)
        .ThenInclude(join => join.Player)
        .FirstOrDefault(nonogram => nonogram.NonogramId == id);
      return View(thisNonogram);
    }

    public ActionResult Edit(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == id);
      return View(thisNonogram);
    }

    [HttpPost]
    public ActionResult Edit (Nonogram nonogram)
    {
      _db.Nonograms.Update(nonogram);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == id);
      return View(thisNonogram);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == id);
      _db.Nonograms.Remove(thisNonogram);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}