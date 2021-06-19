using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Identity; 
using System.Threading.Tasks; 
using System.Security.Claims; 

namespace SweetAndSavory.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly SweetAndSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, SweetAndSavoryContext db)
    {
       _userManager = userManager;
      _db = db;  
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      IEnumerable<Treat> sortedTreats = _db.Treats.OrderBy(treat => treat.Name);
      return View(sortedTreats.ToList());
    }

    public ActionResult Create()
    {
      //ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      // if (FlavorId !=0)
      // {
      //   _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      // }
      // _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
      .Include(treat => treat.JoinEntities)
      .ThenInclude(join => join.Flavor)
      .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }




  }
}