using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nonogram.Models
{
  public class Nonogram
  {
    public int NonogramId { get; set; }
    public int NonogramWidth { get; set; }
    public int NonogramHeight { get; set; }
    // public List<List<int>> NonogramColClues { get; set; }
    // public List<List<int>> NonogramRowClues { get; set; }

    public List<NonogramPlayer> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}