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
  public class CellsController : Controller
  {
    private readonly NonogramPuzzleContext _db;
    static List<Cell> cells = new List<Cell>();

    public CellsController(NonogramPuzzleContext db)
    {

      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Create()
    {
      ViewBag.NonogramId = new SelectList(_db.Nonograms, "NonogramId", "Height");
      return View();
    }

    [HttpPost, ActionName("Create")]
    public ActionResult CreateConfirmed()
    {
      // if (!ModelState.IsValid)
      // {
      //   ViewBag.CategoryId = new SelectList(_db.Nonograms, "NonogramId", "Height");
      //   return View(cell);
      // }
      // else
      // {
        #nullable enable
        List<CellViewModel>? cells =  TempData["cellList"] as List<CellViewModel> ;
        #nullable disable

      for (int i = 0; i < cells.Count(); i++)
      {
      
        Cell cell = new Cell();
        cell.CellState = cells.ElementAt(i).CellState;
        cell.NonogramId = cells.ElementAt(i).NonogramId;
        _db.Cells.Add(cell);
        _db.SaveChanges(); 
      }

      TempData["PuzzeSavedMessage"]="Your Nonogram Puzzle has been saved!";

      return RedirectToAction("Index", "Home");
    } 

    // public IActionResult HandleCellClickSolve(string cellNumber)
    // {

    //   int cllNmbr = int.Parse(cellNumber);

    //   cells.ElementAt(cllNmbr).CellState = (cells.ElementAt(cllNmbr).CellState +1) % 2;

    //   ViewBag.ShowQuestion = false;

    // //   Nonogram model = new BoardViewModel();
    // //   model. = cells;
    // //  //model.Width = int.Parse(width);
    // //   //model.Height = int.Parse(height);
      
    //   return RedirectToAction("Details", "Nonograms", model);
    // }
  }
}