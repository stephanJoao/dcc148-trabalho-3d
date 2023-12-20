using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorScript : MonoBehaviour
{
    [SerializeField] int iterations;
    private int numTiles = 1000;    
    private int[,] tiles;
    public GameObject[] rooms;

    void CreateRoom(int i, int j, char type)
    {
        
        GameObject room = Instantiate(rooms[0]);
        Vector3 pos = new Vector3(i * 150, 0, j * 150);
        room.transform.position = pos;
        room.transform.localScale = new Vector3(10, 10, 10);
        room.transform.parent = transform;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        // probability of changing direction
        float p = 0.3f;
        int direction = 0;
        
        // create vector of possible directions: n, s, e, w
        int[,] directions = { { 0, 1}, { 0, -1}, { 1, 0}, { -1, 0}}; // {x, y}

        // initialize tiles array with 0s
        tiles = new int[numTiles, numTiles];
        for (int m = 0; m < numTiles; m++)
        {
            for (int n = 0; n < numTiles; n++)
            {
                tiles[m, n] = 0;
            }
        }

        int i = 0;
        int j = 0;
        tiles[i, j] = 1; // start at 0, 0

        while (iterations > 0)
        {
            if(Random.value < p)
            {
                direction = Random.Range(0, 4);
            }
            while (true)
            {
                if (i + directions[direction, 0] < numTiles && i + directions[direction, 0] >= 0 && j + directions[direction, 1] < numTiles && j + directions[direction, 1] >= 0)
                {
                    i += directions[direction, 0];
                    j += directions[direction, 1];
                    tiles[i, j] = 1;
                    break;
                }
                else
                {
                    direction = Random.Range(0, 4);
                }
            }
            iterations--;
        }

        // get all prefabs in Assets/Prefabs/Rooms
        rooms = Resources.LoadAll<GameObject>("Prefabs/Rooms");

        for (int m = 0; m < numTiles; m++)
        {
            for (int n = 0; n < numTiles; n++)
            {
                if (tiles[m, n] == 1)
                {
                    CreateRoom(m, n, 'n');
                }
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
