using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Grasp
{
  /// <summary>
  /// Interface for the Solution object.
  /// </summary>
  public interface ISolution
  {
    IValue Value { get; }

    void AddElement(IElement element);

    void RemoveElement(IElement element);

  } // end interface ISolution<IElement>
}
