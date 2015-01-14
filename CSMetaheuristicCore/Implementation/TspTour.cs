using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMetaheuristicCore.Implementation
{
  class TspTour : SolutionBase<double>
  {
    public override double Value
    {
      get { throw new NotImplementedException(); }
    }

    public static bool operator <(TspTour t1, TspTour t2)
    {
      return t1.Value < t2.Value;
    }

    public static bool operator >(TspTour t1, TspTour t2)
    {
      return t1.Value > t2.Value;
    }
  }
}
