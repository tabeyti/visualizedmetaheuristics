using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMetaheuristicCore
{
  public abstract class SolutionBase<T> : ISolution<T>
  {
    protected List<ISolutionElement> solutionList = new List<ISolutionElement>();

    /// <summary>
    /// Returns the value for the solutions.
    /// </summary>
    public abstract T Value { get; }
    
    /// <summary>
    /// Appends an element to the solution list.
    /// </summary>
    /// <param name="element"></param>
    public void AddElement(ISolutionElement element)
    {
      if (null == element)
        throw new Exception("SolutionBase.AddElement: element is null.");
      solutionList.Add(element);

    } // end AddElement()

    /// <summary>
    /// Removes an element from the solution list.
    /// </summary>
    /// <param name="element"></param>
    public void RemoveElement(ISolutionElement element)
    {
      if (null == element)
        throw new Exception("SolutionBase.RemoveElement: element is null.");
      solutionList.Remove(element);

    } // end RemoveElement()

    /// <summary>
    /// Retrieves the solution element at the specified index.
    /// </summary>
    /// <param name="index"></param>
    public ISolutionElement GetElement(int index)
    {
      if (index < 0 || index >= solutionList.Count)
        throw new IndexOutOfRangeException();

      return solutionList[index];

    } // end GetElement()

    /// <summary>
    /// Set's the passed solution to the current solution.
    /// </summary>
    /// <param name="copySol">The solution to copy from.</param>
    public void Set(SolutionBase<T> copyFromSol)
    {
      if (null == copyFromSol)
        return;

      solutionList.Clear();
      for (int i = 0; i < copyFromSol.Count; ++i)
        solutionList.Add(copyFromSol.GetElement(i));

    } // end Set()


  } // end abstract class SolutionBase
}
