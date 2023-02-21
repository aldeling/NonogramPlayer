using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NonogramPuzzle.Models
{
  public class Nonogram
  {
    public int NonogramId { get; set; }

    [Required(ErrorMessage = "The Nonogram board width is required.")]
    public int NonogramWidth { get; set; }

    [Required(ErrorMessage = "The Nonogram board Height is required.")]      public int NonogramHeight { get; set; }
    // public List<List<int>> NonogramColClues { get; set; }
    // public List<List<int>> NonogramRowClues { get; set; }

    public int NonogramDim { get; set; }

    // public Nonogram ()
    // {
    //   NonogramDim = NonogramHeight * NonogramWidth;
    // }
    public List<Cell> Cells { get; set; }
      //this method would run through a nested looping process to determine what the clues are
  }
}