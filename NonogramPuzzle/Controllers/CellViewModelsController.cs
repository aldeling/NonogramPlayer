using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

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
      //Validation related, not tested:
      // if (!ModelState.IsValid)
      // {
      //   return View(model);
      // }
      // else
      // {
  
      Nonogram newNonogram = _db.Nonograms.ToList().LastOrDefault();

      BoardViewModel model = new BoardViewModel();
      model.NonogramId= newNonogram.NonogramId;
      model.Width = newNonogram.NonogramWidth;
      model.Height = newNonogram.NonogramHeight;
      model.BoardDim = newNonogram.NonogramDim;
      cells.Clear();

      if (cells.Count < model.BoardDim)
      {
        for( int i = 0; i < model.BoardDim ; i++)
        {
          cells.Add(new CellViewModel { CellId = i, CellState = 0, NonogramId = model.NonogramId});
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

