using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierreTreats.Models;
using System.Linq;

namespace PierreTreats.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly PierreTreatsContext _db;
    public FlavorsController(PierreTreatsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Flavors.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor, int TreatId)
    {
      _db.Flavors.Add(flavor)
      if (TreatId !=0)
      {
        _db.TreatFlavor.Add(new TreatFlavor(){FlavorId=flavor.FlavorId, TreatId=TreatId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details (int id)
    {
      var thisFlavor = _db.Flavors
        .Include(flavor => flavor.Treats)
        .ThenInclude(join=>join.Treat)
        .FirstOrDefault(flavor=>flavor.FlavorId == id);

      return View(thisFlavor);
    }
