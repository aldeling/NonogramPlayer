using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NonogramPuzzle.Models
{
  public class Player
  {
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public List<NonogramPlayer> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}