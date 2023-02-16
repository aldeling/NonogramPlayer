using System.ComponentModel.DataAnnotations;

namespace NonogramPuzzle.ViewModels
{
  public class BoardViewModel
  {
    [Required]
    public int Width { get; set; }
    
    [Required]
    public int Height { get; set; }

    public List<CellViewModel> CellViewModels { get; set; } = new List<CellViewModel> ();
  }
}