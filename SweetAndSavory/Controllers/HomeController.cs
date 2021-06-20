using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;

namespace SweetAndSavory.Controllers
{
  public class HomeController : Controller
  {
    private readonly SweetAndSavoryContext _db;

    public HomeController(SweetAndSavoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]

    public ActionResult Index()
    {
      ViewBag.TreatMenu = new List<Treat>(_db.Treats.OrderBy(treat => treat.Name));
      ViewBag.FlavorMenu = new List<Flavor>(_db.Flavors.OrderBy(flavor => flavor.Name));
      return View();
    }


  }
}