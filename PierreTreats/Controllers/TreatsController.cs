using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PierreTreats.Models;
using System.Linq;

namespace PierreTreats.Controllers
{
  public class TreatsController : Controller
  {
    private readonly PierreTreatsContext _db;
    public TreatsController(PierreTreatsContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return ViewModels (_db.Treats.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat, int FlavorId)
    {
      _db.Treats.Add(treat)
      if (FlavorId !=0)
      {
        _db.TreatFlavor.Add(new TreatFlavor(){FlavorId=FlavorId, TreatId=treat.TreatId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details (int id)
    {
      var thisTreat = _db.Treats
        .Include(treat => treat.Flavors)
        .ThenInclude(join=>join.Flavor)
        .FirstOrDefault(treat=>treat.TreatId == id);

      return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", "Treats", new { id = treat.TreatId });
    }

    public ActionResult Delete(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treats => treats.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}