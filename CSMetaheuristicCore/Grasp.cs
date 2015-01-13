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
using GraspAlgo;

namespace TSP_Grasp
{
  public class Grasp
  {
    private static double[,] distances;
    private List<Node> searchSpace = new List<Node>();
    public Tour GlobalOptima { get; set; }

    public Grasp(string path)
    {
      Initialize(path);
    }

    private void Initialize(string path)
    {
      searchSpace = GraspIO.ReadFile(path);
      distances = GenerateDistances(searchSpace);
      GlobalOptima = new Tour(searchSpace.ConvertAll(x => (Node)x));

    } // end Initialize()


    /// <summary>
    /// Main workloop for the GRASP algorithm.
    /// </summary>
    public Tour Start(int numIterations)
    {
      for (int i = 0; i < numIterations; ++i)
      {
        Tour solution = GenerateGreedySolution(searchSpace);

        //solution = LocalSearch(solution);

        if (solution < GlobalOptima)
          GlobalOptima = new Tour(solution);
      }

      return GlobalOptima;

    } // end WorkLoop()
    
    /// <summary>
    /// Creates a randomized greedy solution. 
    /// </summary>
    /// <param name="availableNodes"></param>
    /// <returns></returns>
    private Tour GenerateGreedySolution(List<Node> searchSpace)
    {
      List<Node> availableNodes = searchSpace.ConvertAll(x => (Node) x);
      Tour currentSolution = new Tour();
      //Node candidate = GetRandomNode(availableNodes);
      Node candidate = availableNodes[0];
      currentSolution.Add(candidate);
      availableNodes.Remove(candidate);

      while (availableNodes.Count != 0)
      {
        List<Node> rcl = GetRCL(candidate, availableNodes, 3);
        candidate = GetRandomNode(rcl);
        currentSolution.Add(candidate);
        availableNodes.Remove(candidate);
      }

      return currentSolution;

    } // end GenerateGreedySolution();

    /// <summary>
    /// Retrieves a restricted candidate list of the top N nodes in respect
    /// to distance from the provided start node.
    /// </summary>
    /// <param name="startNode">The "from" node.</param>
    /// <param name="nodeList">A list of "to" nodes to evaluate.</param>
    /// <param name="rclSize">The size of the rcl.</param>
    /// <returns></returns>
    private List<Node> GetRCL(Node startNode, List<Node> nodeList, int rclSize)
    {
      List<Node> topNodes = new List<Node>();

      foreach (Node currNode in nodeList)
      {
        if (topNodes.Count < rclSize)
        {
          topNodes.Add(currNode);
          continue;
        }

        List<Node> candidateList = topNodes.ConvertAll(x => (Node) x);
        candidateList.Add(currNode);
        Node longestNode = GetLongestDistanceNode(startNode, candidateList);
        if (currNode != longestNode)
          topNodes[topNodes.IndexOf(longestNode)] = currNode;
        
      }

      return topNodes;

    } // end GetRCL()

    public void PrintTour(Tour tour)
    {
      Console.WriteLine("Length: " + tour.Length);
      Console.WriteLine(tour.ToString() + "\n");
      Console.WriteLine("Total distance: " + tour.Length + "\n\n");

    } // end PrintTour()

    public void PrintTour()
    {
      PrintTour(GlobalOptima);

    } // end PrintTour()

    private static Node GetLongestDistanceNode(Node startNode, List<Node> nodeList)
    {
      // default
      Node smallest = nodeList[0];

      for (int i = 1; i < nodeList.Count; ++i)
      {
        if (distances[startNode.Id, nodeList[i].Id] < distances[startNode.Id, smallest.Id])
        {
          smallest = nodeList[i];
        }
      }

      return smallest;

    } // end GetLongestDistanceNode()

    private Tour LocalSearch(Tour tour)
    {
      return null;

    } // end LocalSearch()

    private static Node GetRandomNode(List<Node> nodes)
    {
      Random rand = new Random();
      return nodes[rand.Next(0, nodes.Count)];

    } // end GetRandomNode()

    /// <summary>
    /// Generates a matrix of distances given a list of location nodes.
    /// </summary>
    /// <param name="nodes"></param>
    private double[,] GenerateDistances(List<Node> nodes)
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
    /// <param name="i">First node.</param>
    /// <param name="j">Second node.</param>
    /// <returns>The distance.</returns>
    public static double GetDistance(Node i, Node j)
    {
      double d1 = Math.Pow((i.X - j.X), 2);
      double d2 = Math.Pow((i.Y - j.Y), 2);

      return (Math.Sqrt(d1 + d2));

    } // end GetDistance()

    public static double GetDistance(int i, int j)
    {
      return distances[i, j];

    } // end GetDistance()

    /// <summary>
    /// Simple location node data structure.
    /// </summary>
    public class Node
    {
      public double X { get; set; }
      public double Y { get; set; }
      public string Name { get; set; }
      public int Id { get; set; }

      public Node(double x, double y, int id)
      {
        this.X = x;
        this.Y = y;
        this.Id = id;
      }

      public Node(double x, double y)
      {
        this.X = x;
        this.Y = y;
      }

    } // end struct Node

    /// <summary>
    /// List of multiple nodes which represent a tour.
    /// </summary>
    public class Tour : List<Node>
    {
      private double length;
      public double Length
      {
        get { return CalculateTourLength(); }
      }

      public Tour()
      {
        
      }

      public Tour(List<Node> list) : base(list)
      {
        
      } // end Tour()

      private double CalculateTourLength()
      {
        double total = 0;
        for (int i = 0; i + 1 < this.Count; ++i)
          total += distances[this[i].Id,this[i+1].Id];

        return total;

      } // end CalculateTourLength()

      public override string ToString()
      {
        return this.Aggregate("", 
          (current, node) => 
            current + ("Id: " + node.Id + " X: " + node.X + " Y: " + node.Y + "\n"));
      }

      public static bool operator <(Tour t1, Tour t2)
      {
        return t1.Length < t2.Length; 
      }

      public static bool operator >(Tour t1, Tour t2)
      {
        return t1.Length > t2.Length;
      }

    } // end class Tour

  } // end class Grasp

}
