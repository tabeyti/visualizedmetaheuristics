using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Grasp
{
  public interface IValue : IComparable
  {
    object GetValue();

    IValue Add(IValue value);

    IValue Substract(IValue value);

    void Clear();

  } // end interface IValue
}
