using Microsoft.AspNetCore.Mvc;
using PierreTreats.Models;
using System.Linq;

namespace PierreTreats.Controllers
{
  public class HomeController : Controller
  {
    private readonly PierreTreatsContext _db;
    public HomeController(PierreTreatsContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      HomeViewModel model = new HomeViewModel();
      model.Treats = _db.Treats.ToList();
      model.Flavors = _db.Flavors.ToList();
      return View(model);
    }
  }
}