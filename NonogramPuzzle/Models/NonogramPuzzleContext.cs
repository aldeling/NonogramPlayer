using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NonogramPuzzle.Models
{
  public class NonogramPuzzleContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Nonogram> Nonograms { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<NonogramPlayer> NonogramPlayers { get; set; }
    public NonogramPuzzleContext(DbContextOptions options) : base(options) { }
  }
}