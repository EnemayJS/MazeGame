using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour {

    public int mazeWidth;
    public int mazeHeight;
    public int coinQuantity;
    public GameObject wall;
    public GameObject floor;
    public GameObject coin;
    public GameObject zombie;
    public GameObject mummy;
    public GameObject player;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.

    System.Random mazeRG;

    Maze maze;
   
    public static BoardManager instance;

    void Awake()
    {
        instance = this;
        
    }

    void BoardSetup()
    {

        mazeRG = new System.Random();
        if (mazeWidth % 2 == 0)
            mazeWidth++;

        if (mazeHeight % 2 == 0)
            mazeHeight++;

        maze = new Maze(mazeWidth, mazeHeight, mazeRG);
        maze.Generate();
       
        DrawBoard();
    }

    void DrawBoard()
    {
        gridPositions.Clear();
        //Instantiate Board and set boardHolder to its transform.
        boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {

                Vector3 position = new Vector3(x, y);

                if (maze.Grid[x, y] == true)
                {
                    //Instantiate floor
                    GameObject instance =
                        Instantiate(floor, position, Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                    gridPositions.Add(position);
                }
                else
                {
                    // instantiate wall
                    GameObject instance =
                        Instantiate(wall, position, Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }



            }
        }
    }


    //RandomPosition returns a random position from our list gridPositions.
    Vector3 RandomPosition()
    {
        //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        int randomIndex = Random.Range(0, gridPositions.Count);

        //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        Vector3 randomPosition = gridPositions[randomIndex];

        //Return the randomly selected Vector3 position.
        return randomPosition;
    }


    public void GenerateCoin()
    {
        //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
        Vector3 randomPosition = RandomPosition();
        Instantiate(coin, randomPosition, Quaternion.identity);
    }

    public void GenerateZombie()
    {
        Vector3 randomPosition = RandomPosition();
        Instantiate(zombie, randomPosition, Quaternion.identity);
    }

    public void GenerateMummy()
    {
        Vector3 randomPosition = RandomPosition();
        Instantiate(mummy, randomPosition, Quaternion.identity);
    }

    public void SetupScene()
    {
        BoardSetup();

        for (int i = 0; i < coinQuantity; i++)
        {
            GenerateCoin();
        }

        GenerateZombie();

        Instantiate(player, new Vector3(1, 1), Quaternion.identity);

    }

    public bool GetMazeGridCell(int x, int y)
    {
        if (x >= mazeWidth || x < 0 || y >= mazeHeight || y <= 0)
        {
            return false;
        }

        return maze.Grid[x, y];
    }

    public bool[,] GetMazeGrid()
    {
        return maze.Grid;
    }

    


}
