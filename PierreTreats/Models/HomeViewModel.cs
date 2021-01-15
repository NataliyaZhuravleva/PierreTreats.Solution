using System.Collections.Generic;

namespace PierreTreats.Models
{
  public class HomeViewModel
  {
    public List<Treat> Treats { get; set; }
    public List<Flavor> Flavors { get; set; }

    public HomeViewModel()
    {
      this.Treats = new List<Treat>();
      this.Flavors = new List<Flavor>();
    }
  }
}