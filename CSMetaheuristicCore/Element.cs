using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Grasp
{
  /// <summary>
  /// This class represents an element within a combinatorial solution.
  /// </summary>
  class Element : IElement
  {
    public IValue Value { get; set; }
  }
}
