using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    private int width;
    private int height;
    private Node[,] nodes;
    private Node startNode;
    private Node endNode;
    private Vector3 endLocation;

    /// <summary>
    /// Create a new instance of PathFinder
    /// </summary>
    /// <param name="searchParameters"></param>
    public PathFinder(Vector3 startLocation, Vector3 endLocation, Node[,] _nodes)
    {
        this.width = _nodes.GetLength(0); 
        this.height = _nodes.GetLength(1); 
        this.nodes = _nodes;
        this.startNode = this.nodes[(int)startLocation.x, (int)startLocation.y];
        this.startNode.State = NodeState.Open;
        this.endNode = this.nodes[(int)endLocation.x, (int)endLocation.y];
        this.endLocation = endLocation;
        Debug.Log("Hello from constructor");
        Debug.Log(this.endLocation.ToString());
        Debug.Log(startLocation.ToString());
    }

    /// <summary>
    /// Attempts to find a path from the start location to the end location based on the supplied SearchParameters
    /// </summary>
    /// <returns>A List of Points representing the path. If no path was found, the returned list is empty.</returns>
    public List<Vector3> FindPath()
    {
        foreach (Node node in nodes)
        {
            node.H = Vector3.Distance(node.Location, endLocation);
            node.State = NodeState.Untested;
        }
        Debug.Log("Hello from FindPath");
        // The start node is the first entry in the 'open' list
        List<Vector3> path = new List<Vector3>();
        Debug.Log("StartNode loc" + startNode.Location.ToString());
        bool success = Search(startNode);

        Debug.Log(success);

        if (success)
        {
            // If a path was found, follow the parents from the end node to build a list of locations
            Node node = this.endNode;
            while (node.ParentNode != null)
            {
                path.Add(node.Location);
                node = node.ParentNode;
            }

            // Reverse the list so it's in the correct order when returned
            path.Reverse();
        }

        return path;
    }

    /// <summary>
    /// Builds the node grid from a simple grid of booleans indicating areas which are and aren't walkable
    /// </summary>
    /// <param name="map">A boolean representation of a grid in which true = walkable and false = not walkable</param>
    

    /// <summary>
    /// Attempts to find a path to the destination node using <paramref name="currentNode"/> as the starting location
    /// </summary>
    /// <param name="currentNode">The node from which to find a path</param>
    /// <returns>True if a path to the destination has been found, otherwise false</returns>
    private bool Search(Node currentNode)
    {
        Debug.Log("Search currentNode loc" + currentNode.Location.ToString());
        // Set the current node to Closed since it cannot be traversed more than once
        currentNode.State = NodeState.Closed;
        Debug.Log("Current node state " + currentNode.State.ToString());
        List<Node> nextNodes = GetAdjacentWalkableNodes(currentNode);

        // Sort by F-value so that the shortest possible routes are considered first
        nextNodes.Sort((node1, node2) => node1.F.CompareTo(node2.F));
        Debug.Log("Hello from search.nextNodes!!! our count is: " + nextNodes.Count().ToString());
        foreach (var nextNode in nextNodes)
        {
            // Check whether the end node has been reached
            if (nextNode.Location == this.endNode.Location)
            {
                return true;
            }
            else
            {
                // If not, check the next set of nodes
                if (Search(nextNode)) // Note: Recurses back into Search(Node)
                    return true;
            }
        }

        // The method returns false if this path leads to be a dead end
        return false;
    }

    /// <summary>
    /// Returns any nodes that are adjacent to <paramref name="fromNode"/> and may be considered to form the next step in the path
    /// </summary>
    /// <param name="fromNode">The node from which to return the next possible nodes in the path</param>
    /// <returns>A list of next possible nodes in the path</returns>
    private List<Node> GetAdjacentWalkableNodes(Node fromNode)
    {
        Debug.Log("Hello from GAWN!! My fromNode loc: " + fromNode.Location.ToString() + " and state: " + fromNode.State.ToString());
        List<Node> walkableNodes = new List<Node>();
        IEnumerable<Vector3> nextLocations = GetAdjacentLocations(fromNode.Location);
        Debug.Log("Hello from GAWN.nextLocations! Our count is: " + nextLocations.ToList().Count().ToString());
        foreach (var location in nextLocations)
        {
            int x = (int)location.x;
            int y = (int)location.y;

            // Stay within the grid's boundaries
            if (x < 0 || x >= this.width || y < 0 || y >= this.height)
                continue;

            Node node = this.nodes[x, y];
            // Ignore non-walkable nodes
            if (!node.IsWalkable)
                continue;

            // Ignore already-closed nodes
            if (node.State == NodeState.Closed)
                continue;

            // Already-open nodes are only added to the list if their G-value is lower going via this route.
            if (node.State == NodeState.Open)
            {
                float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                float gTemp = fromNode.G + traversalCost;
                if (gTemp < node.G)
                {
                    node.ParentNode = fromNode;
                    walkableNodes.Add(node);
                }
            }
            else
            {
                // If it's untested, set the parent and flag it as 'Open' for consideration
                node.ParentNode = fromNode;
                node.State = NodeState.Open;
                walkableNodes.Add(node);
            }
        }
        Debug.Log("Hellofrom WalkableNodes! Our count is: " + walkableNodes.Count().ToString());
        return walkableNodes;
    }

    /// <summary>
    /// Returns the eight locations immediately adjacent (orthogonally and diagonally) to <paramref name="fromLocation"/>
    /// </summary>
    /// <param name="fromLocation">The location from which to return all adjacent points</param>
    /// <returns>The locations as an IEnumerable of Points</returns>
    private static IEnumerable<Vector3> GetAdjacentLocations(Vector3 fromLocation)
    {
        return new Vector3[]
        {

                new Vector3(fromLocation.x-1, fromLocation.y  ),

                new Vector3(fromLocation.x,   fromLocation.y+1),

                new Vector3(fromLocation.x+1, fromLocation.y  ),

                new Vector3(fromLocation.x,   fromLocation.y-1)
        };
    }
}
