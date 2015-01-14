using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSMetaheuristicCore;

namespace CSMetaheuristicCore
{
  interface IMetaheuristic<T>
  {
    ISolution<T> Result { get; }

    Task Run();

    void Initialize(params string[] args);

  } // end interface IMetaheuristic
}
