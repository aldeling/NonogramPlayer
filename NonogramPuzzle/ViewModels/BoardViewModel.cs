using System.ComponentModel.DataAnnotations;
using NonogramPuzzle.Models;

namespace NonogramPuzzle.ViewModels
{
  public class BoardViewModel
  {
    [Required]
    public int Width { get; set; }
    
    [Required]
    public int Height { get; set; }
    //public int NonogramId { get; set;}
    public Nonogram Nonogram { get; set; }

    public List<CellViewModel> CellViewModels { get; set; } = new List<CellViewModel> ();
  }
}