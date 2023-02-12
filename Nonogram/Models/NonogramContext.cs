using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Nonogram.Models
{
  public class NonogramContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Nonogram> Nonograms { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<NonogramPlayer> NonogramPlayers { get; set; }
    public NonogramContext(DbContextOptions options) : base(options) { }
  }
}