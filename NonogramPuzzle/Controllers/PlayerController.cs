// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc;
// using Nonogram.Models;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using System.Threading.Tasks;
// using System.Security.Claims;

// namespace Nonogram.Controllers
// {
//   [Authorize]
//   public class PlayerController : Controller
//   {
//     private readonly NonogramContext _db;
//     private readonly UserManager<ApplicationUser> _userManager;
//     public PlayerController(UserManager<ApplicationUser> userManager, NonogramContext db)
//     {
//       _userManager = userManager;
//       _db = db;
//     }

//     public async Task<ActionResult> Index()
//     {
//       string userId = userId.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//       ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
//     }

//     public ActionResult Create()
//     {
//       return View();
//     }

//     [HttpPost]
//     public async Task<ActionResult> Create(Player player)
//     {

//     }
//   }
// }