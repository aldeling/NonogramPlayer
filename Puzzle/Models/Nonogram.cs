using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Puzzle.Models
{
  public class Nonogram
  {
    public int NonogramId { get; set; }
    public int NonogramWidth { get; set; }
    public int NonogramHeight { get; set; }
    // public List<List<int>> NonogramColClues { get; set; }
    // public List<List<int>> NonogramRowClues { get; set; }

    public List<NonogramPlayer> JoinEntities { get; }
    // public ApplicationUser User { get; set; }

    //public static List<List<int>> GetNonogramClues()
      //this method would run through a nested looping process to determine what the clues are
  }
}