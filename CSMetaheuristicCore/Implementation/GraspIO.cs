// ============================================================================
// GraspIO Class
// ============================================================================
// This class handles the input and out put for the Grasp Algorithm
//
// ============================================================================

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using CSMetaheuristicCore;

namespace CSMetaheuristicCore.Implementation
{
	public class GraspIO
	{
    // ==== GetPaths ======================================================
    //
    // Description:
    //		This function retrieves a list file paths for the specified
    //      directory.
    //
    // Input:
    //		string			dir	- the directory to retrieve the files from
    //
    // Output: 
    //		A string array containing the file paths
    //
    // ====================================================================

    public static string[] GetPaths(string dir)
    {
      return Directory.GetFiles(dir);

    } // end GetPaths(string dir)


    // ==== ReadHeader ====================================================
    //
    // Description:
    //		This function retrieves the header of the TSP file.
    //
    // Input:
    //		string			path	- the path to the file to read
    //
    // Output: 
    //		A string containing the header
    //
    // ====================================================================

    public static string ReadHeader(string path)
    {
      StreamReader reader = new StreamReader(path);
      string header = "";
      string line = "";

      // traverses down to node data
      while ("NODE_COORD_SECTION" != (line = reader.ReadLine()))
      {
      header += line + Environment.NewLine;
      }

      return header;

    } // end ReadHeader(string path)
		

		
		// ==== ReadFile ======================================================
		//
		// Description:
		//		This function opens a TSP file and populates the dictionary
		//		based upon the contents.  It handles its traversal and data
		//		delimiting based upon the TSBLIB keywords found in the .tsp
		//		files.
		//
		// Input:
		//		string			path	- the path to the file to read
		//
		// Output: 
		//		A populated dictionary of node/coordinate items
		//
		// ====================================================================
		
		public static List<TspElement> ReadFile(string path)
		{
			string tempString = "";
			string[] tempStringArray;
      TspElement node;
      List<TspElement> nodeList = new List<TspElement>();
      StreamReader reader = new StreamReader(path);
			
			// traverses down to node data
			while ("NODE_COORD_SECTION" != reader.ReadLine())
			{}
				
			while ("EOF" != (tempString = reader.ReadLine()))
			{
			  // splits data string into node/coordinate components
			  tempStringArray = tempString.Split(new string[]{" "},
          StringSplitOptions.RemoveEmptyEntries);
				
        // add node to the list
        node = new TspElement(
          Convert.ToInt32(tempStringArray[0])-1,
          Convert.ToDouble(tempStringArray[1]),
          Convert.ToDouble(tempStringArray[2])
          );
        nodeList.Add(node);
			}
				
			reader.Close();
			
			return nodeList;
		
		} // end ReadFile(string path);
		
	} // end class GraspIO

}
