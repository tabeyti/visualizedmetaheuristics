using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMetaheuristicCore
{
  /// <summary>
  /// Interface for the SolutionBase object.
  /// </summary>
  public interface ISolution<out T>
  {
    T Value { get; }

    void AddElement(ISolutionElement element);

    void RemoveElement(ISolutionElement element);

    ISolutionElement GetElement(int index);

  } // end interface ISolution<IElement>
}
