using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Represents the search state of a Node

public enum NodeState
{
   
    // The node has not yet been considered in any possible paths
   
    Untested,
    
    // The node has been identified as a possible step in a path
   
    Open,
    
    // The node has already been included in a path and will not be considered again
    
    Closed
}
