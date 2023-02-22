using Microsoft.EntityFrameworkCore;

namespace NonogramPuzzle.Models
{
  public class NonogramPuzzleContext : DbContext
  {
    public DbSet<Nonogram> Nonograms { get; set; }
    public DbSet<Cell> Cells { get; set; }
    public NonogramPuzzleContext(DbContextOptions options) : base(options) { }
  }
}