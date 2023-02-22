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

      //calcualing dimension for empty boad with clues included

      int width = thisNonogram.NonogramWidth;
      int height = thisNonogram.NonogramHeight;
      int boardSize = width * height;//thisNonogram.NonogramDim;
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
      thisNonogram.sovlingBoardHeight = maxHeight + height;
      thisNonogram.solvigBoardDim = (thisNonogram.solvingBoardWidth * thisNonogram.sovlingBoardHeight);
      
      thisNonogram.Cells.Clear();

      if (thisNonogram.Cells.Count < thisNonogram.solvigBoardDim)
      {
        for( int i = 0; i < thisNonogram.solvigBoardDim; i++)
        {
          thisNonogram.Cells.Add(new Cell { CellId = i, CellState = 0, NonogramId = id});
        }
        // thisNonogram.Cells.ElementAt(0).CellState = 1;
        // thisNonogram.Cells.ElementAt(1).CellState = 1;
        // thisNonogram.Cells.ElementAt(7).CellState = 1;
        // thisNonogram.Cells.ElementAt(8).CellState = 1;
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
  }
}