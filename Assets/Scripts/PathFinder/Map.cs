using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map  {

    private int width;
    private int height;
    private Node[,] nodes;

    public Node[,] Nodes
    {
        get { return nodes; }
    }
    
    public Map(bool[,] map)
    {
        this.width = map.GetLength(0); Debug.Log(map.GetLength(0).ToString());
        this.height = map.GetLength(1); Debug.Log(map.GetLength(1).ToString());
        this.nodes = new Node[this.width, this.height];
        for (int y = 0; y < this.height; y++)
        {
            for (int x = 0; x < this.width; x++)
            {
                this.nodes[x, y] = new Node(x, y, map[x, y]);
                Debug.Log("Initialized Node: " + this.nodes[x, y].Location.ToString() + "IsWalkable: " + this.nodes[x, y].IsWalkable.ToString());
            }
        }
    }
}
