using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Grasp
{
  class Program
  {
    static void Main(string[] args)
    {
      Grasp g = new Grasp(ConfigurationManager.AppSettings["FilePath"]);

      g.PrintTour();
      Grasp.Tour tour = g.Start(Int32.Parse(ConfigurationManager.AppSettings["Iterations"]));
      g.PrintTour(tour);

    }
  }
}
