using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NonogramPuzzle.ViewModels;

namespace NonogramPuzzle.Controllers
{
  public class CellViewModelsController: Controller
  {
    static List<CellViewModel> cells = new List<CellViewModel>();
    const int gridSize = 28;

    public IActionResult Index()
    {
      if (cells.Count < gridSize)
      {
        for( int i = 0; i < gridSize ; i++)
        {
          cells.Add(new CellViewModel { CellId = i, CellState = 0 });
        }
      }

      return View(cells);
    }

    public IActionResult HandleCellClick(string cellNumber)
    {
      int cllNmbr = int.Parse(cellNumber);

      cells.ElementAt(cllNmbr).CellState = (cells.ElementAt(cllNmbr).CellState +1) % 2;

      return View("Index", cells);
    }
  }
}

