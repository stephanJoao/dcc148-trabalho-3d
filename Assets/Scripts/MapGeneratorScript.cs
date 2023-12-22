using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGeneratorScript : MonoBehaviour
{
    [SerializeField] int iterations;
    [SerializeField] GameObject player;
    private int numTiles = 100;    
    private int[,] tiles;
    public GameObject[] rooms;

    void CreateRoom(int i, int j, int[] type, bool lastRoom)
    {
        GameObject room;

        if (type[0] == 0 && type[1] == 1 && type[2] == 0 && type[3] == 0)
        {
            room = Instantiate(rooms[0]); 
        }
        else if (type[0] == 1 && type[1] == 0 && type[2] == 0 && type[3] == 0)
        {
            room = Instantiate(rooms[1]);
        }
        else if (type[0] == 0 && type[1] == 0 && type[2] == 0 && type[3] == 1)
        {
            room = Instantiate(rooms[2]);
        }
        else if (type[0] == 0 && type[1] == 0 && type[2] == 1 && type[3] == 0)
        {
            room = Instantiate(rooms[3]);
        }
        else if (type[0] == 1 && type[1] == 1 && type[2] == 0 && type[3] == 0)
        {
            room = Instantiate(rooms[4]);
        }
        else if (type[0] == 0 && type[1] == 1 && type[2] == 0 && type[3] == 1)
        {
            room = Instantiate(rooms[5]);
        }
        else if (type[0] == 0 && type[1] == 1 && type[2] == 1 && type[3] == 0)
        {
            room = Instantiate(rooms[6]);
        }
        else if (type[0] == 1 && type[1] == 0 && type[2] == 0 && type[3] == 1)
        {
            room = Instantiate(rooms[7]);
        }
        else if (type[0] == 1 && type[1] == 0 && type[2] == 1 && type[3] == 0)
        {
            room = Instantiate(rooms[8]);
        }
        else if (type[0] == 0 && type[1] == 0 && type[2] == 1 && type[3] == 1)
        {
            room = Instantiate(rooms[9]);
        }
        else if (type[0] == 1 && type[1] == 1 && type[2] == 0 && type[3] == 1)
        {
            room = Instantiate(rooms[10]);
        }
        else if (type[0] == 1 && type[1] == 1 && type[2] == 1 && type[3] == 0)
        {
            room = Instantiate(rooms[11]);
        }
        else if (type[0] == 0 && type[1] == 1 && type[2] == 1 && type[3] == 1)
        {
            room = Instantiate(rooms[12]);
        }
        else if (type[0] == 1 && type[1] == 0 && type[2] == 1 && type[3] == 1)
        {
            room = Instantiate(rooms[13]);
        }
        else if (type[0] == 1 && type[1] == 1 && type[2] == 1 && type[3] == 1)
        {
            room = Instantiate(rooms[14]);
        }
        else
        {
            room = Instantiate(rooms[15]);
        }
       
        Vector3 pos = new Vector3(i * 15, 0, j * 15);
        room.transform.position = pos;
        room.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        room.transform.parent = transform;

        
        Light light = room.transform.GetChild(room.transform.childCount - 1).GetChild(0).GetComponent<Light>();
        
        if(lastRoom)
        {
            light.intensity = 4.5f;
            light.range = 40.0f;
            room.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            light.intensity = Random.Range(0.2f, 0.6f);
            light.range = 40.0f;
            room.GetComponent<BoxCollider>().enabled = false;
        }       
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        // get player object
        player = GameObject.FindWithTag("Player");
        
        // set player position to middle of first room
        player.transform.position = new Vector3(transform.position.x + 17.5f, 0, transform.position.z + 22.5f);
        
        // probability of changing direction
        float p = 0.4f;
        int direction = 0;
        
        // create vector of possible directions: n, s, e, w
        int[,] directions = {{0, 1}, {0, -1}, {1, 0}, {-1, 0}}; // {x, y}

        // initialize tiles array with 0s
        tiles = new int[numTiles, numTiles];
        for (int m = 0; m < numTiles; m++)
        {
            for (int n = 0; n < numTiles; n++)
            {
                tiles[m, n] = 0;
            }
        }

        // start at 0, 0
        int i = 0;
        int j = 0;
        tiles[i, j] = 1; 

        // create path
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

        // create auxiliary tiles
        int[,] auxTiles = new int[numTiles+2, numTiles+2];
        for (int m = 0; m < numTiles+2; m++)
        {
            for (int n = 0; n < numTiles+2; n++)
            {
                if (m == 0 || n == 0 || m == numTiles+1 || n == numTiles+1)
                {
                    auxTiles[m, n] = 0;
                }
                else
                {
                    auxTiles[m, n] = tiles[m-1, n-1];
                }
            }
        }
        
        // get all prefabs in Assets/Prefabs/Rooms
        rooms = Resources.LoadAll<GameObject>("Prefabs/Rooms");

        // create rooms
        for (int m = 1; m < numTiles + 1; m++)
        {
            for (int n = 1; n < numTiles + 1; n++)
            {
                if (auxTiles[m, n] == 1)
                {
                    CreateRoom(m, n, new int[] { auxTiles[m+1, n], auxTiles[m, n+1], auxTiles[m-1, n], auxTiles[m, n-1] }, (m - 1 == i && n - 1 == j));
                }
            }
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}