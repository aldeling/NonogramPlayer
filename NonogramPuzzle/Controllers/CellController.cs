// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// using NonogramPuzzle.Models;

// namespace NonogramPuzzle.Controllers
// {
//   public class CellController : Controller
//   {
//     //grid size user inputted
//     static List<Cell> cells = new List<Cell>();
//     int gridSize = 25;
//     public IActionResult Index()
//     {
//       for (int i = 0; i < gridSize; i++)
//       {
//       cells.Add(new Cell { CellId = 0, CellState = 0});
//       //how many cells
//       }
      
//       return View(cells);
//     }

//     public IActionResult HandleCellClick(string cellNumber)
//     {
//       int cll = int.Parse(cellNumber);

//       cells.ElementAt(cll).CellState = (cells.ElementAt(cll).CellState + 1) % 3;

//       return RedirectToAction("Index", cells);
//     }
//   }
// }