using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Grasp
{
  public abstract class Solution : ISolution
  {
    private List<IElement> solutionList = new List<IElement>();

    public abstract IValue Value { get; }

    /// <summary>
    /// Appends an element to the solution list.
    /// </summary>
    /// <param name="element"></param>
    public void AddElement(IElement element)
    {
      if (null == element)
        throw new Exception("Solution.AddElement: element is null.");
      solutionList.Add(element);

    } // end AddElement()

    /// <summary>
    /// Removes an element from the solution list.
    /// </summary>
    /// <param name="element"></param>
    public void RemoveElement(IElement element)
    {
      if (null == element)
        throw new Exception("Solution.RemoveElement: element is null.");
      solutionList.Remove(element);

    } // end RemoveElement()

    /// <summary>
    /// Constructs a value for the solution by iterating over the list
    /// of elements to the solution.
    /// </summary>
    /// <returns>A reference to the value.</returns>
    protected IValue GenerateValue()
    {
      Value.Clear();

      foreach (IElement elem in solutionList)
      {
        Value.Add(elem.Value);
      }

      return Value;

    } // end GenerateValue()

  } // end abstract class Solution
}
