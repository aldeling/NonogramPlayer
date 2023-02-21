using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using NonogramPuzzle.Models;
using NonogramPuzzle.ViewModels;

namespace NonogramPuzzle.Controllers
{
  public class NonogramsController : Controller
  {
    private readonly NonogramPuzzleContext _db;
    
    public NonogramsController(NonogramPuzzleContext db)
    {

      _db = db;
    }

    public ActionResult Index()
    {
      List<Nonogram> model = _db.Nonograms.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Nonogram nonogram)
    {
       nonogram.NonogramDim = nonogram.NonogramWidth * nonogram.NonogramWidth;
      _db.Nonograms.Add(nonogram);
      _db.SaveChanges();
  
      return RedirectToAction("Build","CellViewModels");
    }

    public ActionResult Details(int id)
    {
      // Nonogram thisNonogram = _db.Nonograms
      //   .FirstOrDefault(nonogram => nonogram.NonogramId == id);
      // return View(thisNonogram);

      Nonogram thisNonogram = _db.Nonograms.Include(nono => nono.Cells)
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