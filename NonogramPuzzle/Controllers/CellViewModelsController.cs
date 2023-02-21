using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NonogramPuzzle.Models;
using NonogramPuzzle.ViewModels;

namespace NonogramPuzzle.Controllers
{
  public class CellViewModelsController: Controller
  {
    static List<CellViewModel> cells = new List<CellViewModel>();

    private readonly NonogramPuzzleContext _db;

    public CellViewModelsController(NonogramPuzzleContext db)
    {

      _db = db;
    }

    public IActionResult Index()
    {
      

      return View(cells);
    }

    public IActionResult Build()
    {
      //ViewBag.ShowQuestion = true;
      ViewBag.NonogrmId = new SelectList( _db.Nonograms, "NonogramId", "NonogramId");
      ViewBag.NonogramDim = new SelectList(_db.Nonograms,"NonogramDim", "NonogramDim");
      ViewBag.NonogramWidth = new SelectList(_db.Nonograms,"NonogramWidth", "NonogramWidth");

      return View();
    }

    [HttpPost]
    public IActionResult Build(int NonogrmId, int NonogramDim, int NonogramWidth)
    {

      // if (!ModelState.IsValid)
      // {
      //   return View(model);
      // }
      // else
      // {
    
        BoardViewModel model = new BoardViewModel();
        model.Width = NonogramWidth;
        model.Height = NonogramDim / NonogramWidth;

        int gridSize = NonogramDim;
        cells.Clear();

        if (cells.Count < gridSize)
        {
          for( int i = 0; i < gridSize ; i++)
          {
            cells.Add(new CellViewModel { CellId = i, CellState = 0, NonogramId = NonogrmId});
          }
          model.CellViewModels = cells;
        }

        ViewBag.ShowQuestion = false;
        
        return View (model);
      // }
    }

    
    public IActionResult SavePuzzle(string save)
    {

      for (int i = 0; i < cells.Count(); i++)
      {
      
        Cell cell = new Cell();
        cell.CellState = cells.ElementAt(i).CellState;
        cell.NonogramId = cells.ElementAt(i).NonogramId;
        _db.Cells.Add(cell);
        _db.SaveChanges(); 
      }
      
      // BoardViewModel model = new BoardViewModel();
      // model.CellViewModels = cells;

      // TempData["cellList"] = cells;

      //return RedirectToAction("Create", "Cells");

      return RedirectToAction("Index", "Home");

    }

    public IActionResult HandleCellClick(string cellNumber ,string height, string width)
    {

      int cllNmbr = int.Parse(cellNumber);

      cells.ElementAt(cllNmbr).CellState = (cells.ElementAt(cllNmbr).CellState +1) % 2;

      ViewBag.ShowQuestion = false;

      BoardViewModel model = new BoardViewModel();
      model.CellViewModels = cells;
      model.Width = int.Parse(width);
      model.Height = int.Parse(height);
      
      return View("Build", model);
    }

  }
}

