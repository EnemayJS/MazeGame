using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Node parentNode;
   
    // The node's location in the grid  
    public Vector3 Location { get; private set; }

    // True when the node may be traversed, otherwise false
    public bool IsWalkable { get; set; }
  
    // Cost from start to here
    public float G { get; private set; }

    // Estimated cost from here to end
    public float H { get;  set; }

    // Flags whether the node is open, closed or untested by the PathFinder
    public NodeState State { get; set; }

    // Estimated total cost (F = G + H)
    public float F
    {
        get { return this.G + this.H; }
    }

    //Gets or sets the parent node. The start node's parent is always null.

    public Node ParentNode
    {
        get { return this.parentNode; }
        set
        {
            // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
            this.parentNode = value;
            this.G = this.parentNode.G + GetTraversalCost(this.Location, this.parentNode.Location);
        }
    }

  
    // Creates a new instance of Node.
  
    // <param name="x">The node's location along the X axis</param>
    // <param name="y">The node's location along the Y axis</param>
    // <param name="isWalkable">True if the node can be traversed, false if the node is a wall</param>
    // <param name="endLocation">The location of the destination node</param>

    public Node(int x, int y, bool isWalkable)//, Vector3 endLocation)
    {
        this.Location = new Vector3(x, y);
        this.State = NodeState.Untested;
        this.IsWalkable = isWalkable;
        //this.H = GetTraversalCost(this.Location, endLocation);
        this.G = 0;
    }

    /*public override string ToString()
    {
        return string.Format("{0}, {1}: {2}", this.Location.X, this.Location.Y, this.State);
    }*/

    //Gets the distance between two points
  
    internal static float GetTraversalCost(Vector3 location, Vector3 otherLocation)
    {
        
        return Vector3.Distance(location, otherLocation);
    }
}
