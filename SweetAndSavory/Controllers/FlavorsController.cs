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
  public class FlavorsController : Controller
  {
    private readonly SweetAndSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, SweetAndSavoryContext db)
    {
       _userManager = userManager;
      _db = db;  
    }

    [AllowAnonymous]
    public ActionResult Index()
    {
      IEnumerable<Flavor> sortedFlavors = _db.Flavors.OrderBy(flavor => flavor.Name);
      return View(sortedFlavors.ToList());
    }


  }
}