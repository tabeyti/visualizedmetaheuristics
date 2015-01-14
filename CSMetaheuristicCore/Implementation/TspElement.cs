using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSMetaheuristicCore;

namespace CSMetaheuristicCore.Implementation
{
  public class TspElement : ISolutionElement
  {
    public double X { get; set; }
    public double Y { get; set; }

    public int Id { get; set; }

    public TspElement(int id, double x, double y)
    {
      Id = id;
      X = x;
      Y = y;

    } // end TspElement

  } // end class TspElement
}
