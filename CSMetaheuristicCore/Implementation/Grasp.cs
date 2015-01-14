///
/// http://3lbmonkeybrain.blogspot.com/2012/10/raphaeljs-typescript-raphaelts.html
/// 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace CSMetaheuristicCore.Implementation
{
  public class Grasp : IMetaheuristic<double>
  {
    private static double[,] distances;
    private List<TspElement> searchSpace = new List<TspElement>();
    public TspTour GlobalOptima { get; set; }
    private int numIterations = 0;

    public void Initialize(params string[] args)
    {
      searchSpace = GraspIO.ReadFile((string)args[0]);
      numIterations = int.Parse((string) args[1]);
      distances = GenerateDistances(searchSpace);
      GlobalOptima = new TspTour(searchSpace.ConvertAll(x => (TspElement)x));

    } // end Initialize()


    public ISolution<double> Result { get; private set; }

    public Task Run()
    {
      for (int i = 0; i < numIterations; ++i)
      {
        TspTour solution = GenerateGreedySolution(searchSpace);

        //solution = LocalSearch(solution);

        if (solution < GlobalOptima)
          GlobalOptima = new TspTour(solution);
      }

    } // end Run()

    
    /// <summary>
    /// Creates a randomized greedy solution. 
    /// </summary>
    /// <param name="availableNodes"></param>
    /// <returns></returns>
    private TspTour GenerateGreedySolution(List<TspElement> searchSpace)
    {
      List<TspElement> availableNodes = searchSpace.ConvertAll(x => (TspElement) x);
      TspTour currentSolution = new TspTour();
      //TspElement candidate = GetRandomNode(availableNodes);
      TspElement candidate = availableNodes[0];
      currentSolution.AddElement(candidate);
      availableNodes.Remove(candidate);

      while (availableNodes.Count != 0)
      {
        List<TspElement> rcl = GetRCL(candidate, availableNodes, 3);
        candidate = GetRandomNode(rcl);
        currentSolution.AddElement(candidate);
        availableNodes.Remove(candidate);
      }

      return currentSolution;

    } // end GenerateGreedySolution();

    /// <summary>
    /// Retrieves a restricted candidate list of the top N nodes in respect
    /// to distance from the provided start TspElement.
    /// </summary>
    /// <param name="startNode">The "from" TspElement.</param>
    /// <param name="nodeList">A list of "to" nodes to evaluate.</param>
    /// <param name="rclSize">The size of the rcl.</param>
    /// <returns></returns>
    private List<TspElement> GetRCL(TspElement startNode, List<TspElement> nodeList, int rclSize)
    {
      List<TspElement> topNodes = new List<TspElement>();

      foreach (TspElement currNode in nodeList)
      {
        if (topNodes.Count < rclSize)
        {
          topNodes.Add(currNode);
          continue;
        }

        List<TspElement> candidateList = topNodes.ConvertAll(x => (TspElement) x);
        candidateList.Add(currNode);
        TspElement longestNode = GetLongestDistanceNode(startNode, candidateList);
        if (currNode != longestNode)
          topNodes[topNodes.IndexOf(longestNode)] = currNode;
        
      }

      return topNodes;

    } // end GetRCL()

    public void PrintTour(TspTour tspTour)
    {
      Console.WriteLine("Length: " + tspTour.Value);
      Console.WriteLine(tspTour.ToString() + "\n");
      Console.WriteLine("Total distance: " + tspTour.Value + "\n\n");

    } // end PrintTour()

    public void PrintTour()
    {
      PrintTour(GlobalOptima);

    } // end PrintTour()

    private static TspElement GetLongestDistanceNode(TspElement startNode, List<TspElement> nodeList)
    {
      // default
      TspElement smallest = nodeList[0];

      for (int i = 1; i < nodeList.Count; ++i)
      {
        if (distances[startNode.Id, nodeList[i].Id] < distances[startNode.Id, smallest.Id])
        {
          smallest = nodeList[i];
        }
      }

      return smallest;

    } // end GetLongestDistanceNode()

    private TspTour LocalSearch(TspTour tspTour)
    {
      return null;

    } // end LocalSearch()

    private static TspElement GetRandomNode(List<TspElement> nodes)
    {
      Random rand = new Random();
      return nodes[rand.Next(0, nodes.Count)];

    } // end GetRandomNode()

    /// <summary>
    /// Generates a matrix of distances given a list of location nodes.
    /// </summary>
    /// <param name="nodes"></param>
    private double[,] GenerateDistances(List<TspElement> nodes)
    {
      double[,] dists = new double[nodes.Count, nodes.Count];

      for (int i = 0; i < nodes.Count; ++i)
      {
        for (int j = 0; j < nodes.Count; ++j)
        {
          // if the distance has already been calculated, use that value
          if (dists[j, i] != 0)
          {
            dists[i, j] = dists[j, i];
            continue;
          }

          dists[i, j] = GetDistance(nodes[i], nodes[j]);
        }
      }

      return dists;

    } // end GenerateDistances()
    
    /// <summary>
    /// Retrives the euclidian distance between two nodes.
    /// </summary>
    /// <param name="i">First TspElement.</param>
    /// <param name="j">Second TspElement.</param>
    /// <returns>The distance.</returns>
    public static double GetDistance(TspElement i, TspElement j)
    {
      MathExtensions.EuclidianDistance(i.X, i.Y, j.X, j.Y);

    } // end GetDistance()

    public static double GetDistance(int i, int j)
    {
      return distances[i, j];

    } // end GetDistance()

  } // end class Grasp

}
