using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Math;

namespace CSMetaheuristicCore
{
  class MathExtensions
  {

    public static double EuclidianDistance(double x1, double y1, double x2, double y2)
    {
      double d1 = Math.Pow((x1 - x2), 2);
      double d2 = Math.Pow((y1 - y2), 2);

      return (Math.Sqrt(d1 + d2));
      
    } // end EuclidianDistance()

  }
}
