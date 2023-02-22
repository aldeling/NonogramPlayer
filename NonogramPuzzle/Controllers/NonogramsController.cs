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
    static List<Cell> cellList = new List<Cell>();
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
      nonogram.NonogramDim = nonogram.NonogramWidth * nonogram.NonogramHeight;
      _db.Nonograms.Add(nonogram);
      _db.SaveChanges();

      return RedirectToAction("Build","CellViewModels");
    }

    public ActionResult Details(int id)
    {
      Nonogram thisNonogram = _db.Nonograms.Include(nono => nono.Cells)
      .FirstOrDefault(nonogram => nonogram.NonogramId == id);

      //calculating dimension for empty board with clues included

      int width = thisNonogram.NonogramWidth;
      int height = thisNonogram.NonogramHeight;
      int boardSize = thisNonogram.NonogramDim;//width * height;//thisNonogram.NonogramDim;
      int maxHeight = 0;
      int maxWidth = 0;

      //calculation board height, accounting for max. number of clues in the columns
      // i = rows/Height, j = columns/width
      for(int j = 0; j < width ; j++)
      {
        int maxColClueCount = 0;

        for(int i = j ; i <= (boardSize - (width - j)); i = (i + width ))
        {
          int previousCell = i;
          if (i >= width)
          {
            previousCell = i - width;
          }
          if (((thisNonogram.Cells.ElementAt(i).CellState == 1) && (i < width)) || ((thisNonogram.Cells.ElementAt(i).CellState == 1) && (thisNonogram.Cells.ElementAt(previousCell).CellState == 0)))
          {
            maxColClueCount ++;
          }
        }
        if( maxHeight < maxColClueCount)
        {
          maxHeight = maxColClueCount;
        }
      }

      //calculation board width, account for max. clues in the rows
      int maxRowClueCount = 0;

      for(int i = 0 ; i < boardSize; i ++)
      {
        if ( i % (width) == 0 && i != 0)
        {
          if(maxWidth < maxRowClueCount)
          {
            maxWidth = maxRowClueCount;
          }

          maxRowClueCount = 0;
        }
        int previousCell = i;
        if (i % width != 0)
        {
          previousCell = i-1;
        }

        if (((thisNonogram.Cells.ElementAt(i).CellState == 1) && (i % width == 0)) || (thisNonogram.Cells.ElementAt(i).CellState == 1) && (thisNonogram.Cells.ElementAt(previousCell).CellState == 0))
          {
            maxRowClueCount++;
          }  
      }

      thisNonogram.solvingBoardWidth = maxWidth + width;
      thisNonogram.solvingBoardHeight = maxHeight + height;
      thisNonogram.solvingBoardDim = (thisNonogram.solvingBoardWidth * thisNonogram.solvingBoardHeight);

      //Saving SolvingBoard Width, Height, and Dim to data base
      _db.Nonograms.Update(thisNonogram);
      _db.SaveChanges();
      
      //clearing the static List object cellList, to preventing additional cells from being add when
      //cell states are being updated and puzzle board refreshes.
      cellList.Clear();
    
      if (cellList.Count < thisNonogram.solvingBoardDim)
      {
        for( int i = 0; i < thisNonogram.solvingBoardDim; i++)
        {
          cellList.Add(new Cell { CellId = i, CellState = 0, NonogramId = id});
        }

        thisNonogram.Cells = cellList;
      }

      for( int j = 0 ; j < maxWidth ; j++)
      {
        int i = j + thisNonogram.solvingBoardWidth;
        thisNonogram.Cells.ElementAt(j).CellState = 1;
        thisNonogram.Cells.ElementAt(i).CellState = 1; 
      }

      //This will not be saved in the database
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

    public IActionResult HandleCellClickSolve(string id, string cellNumber)
    {
      int cllNmbr = int.Parse(cellNumber);
      int nonoGramId = int.Parse(id);

      Nonogram thisNonogram = _db.Nonograms.FirstOrDefault(nonogram => nonogram.NonogramId == nonoGramId);
      
      Nonogram model = new Nonogram();
      
      cellList.ElementAt(cllNmbr).CellState = (cellList.ElementAt(cllNmbr).CellState +1) % 2;

      model.NonogramId = thisNonogram.NonogramId;
      model.solvingBoardHeight = thisNonogram.solvingBoardHeight;
      model.solvingBoardWidth = thisNonogram.solvingBoardWidth;
      model.solvingBoardDim = thisNonogram.solvingBoardDim;
      model.NonogramWidth = thisNonogram.NonogramWidth;
      model.NonogramHeight = thisNonogram.NonogramHeight;
      model.NonogramDim = thisNonogram.NonogramDim;
      model.Cells = cellList;

      return View("Details", model);
    }

    // public IActionResult HandleCellClick(string cellNumber ,string height, string width)
    // {
    //   int cllNmbr = int.Parse(cellNumber);

    //   cellsList.ElementAt(cllNmbr).CellState = (cellsList.ElementAt(cllNmbr).CellState +1) % 2;

    //   BoardViewModel model = new BoardViewModel();
    //   model.CellViewModels = cellsList;
    //   model.Width = int.Parse(width);
    //   model.Height = int.Parse(height);
      
    //   return View("Build", model);
    // }
  }
}