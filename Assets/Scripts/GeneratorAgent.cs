using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorAgent : Agent
{
    public Cell[,] grid;
    private int gridsize;
    public List<Room> rooms;
    public Vector2Int size;
    Vector2Int currPos;
    public int startPos = 0;
    public Vector2 offset;
    List<Cell> board;
    int currentCell; 
    public Targets Target;
    public GameObject Emptyroom;
    public GameObject Treasureroom;
    public GameObject Bossroom;
    public GameObject Enemyroom;

    [SerializeField]
    GameObject cubePrefab;
    [SerializeField]
    Material redMaterial;
    [SerializeField]
    Material blueMaterial;

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    public class Room
    {
        public Vector2Int location;
        public roomType roomtype;

        public enum roomType
        {
            None,
            Empty,              
            Enemy,             
            Boss,       
            Treasure         
        };

        public Room(Vector2Int location, roomType roomtype) 
        {
            this.location = location;
            this.roomtype = roomtype;
        }
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(0,4,0); 

        // for (int j = 0; j < rooms.Count; j++)
        // {
        //     rooms[j].roomtype = Room.roomType.None;
        // }
        currentCell = startPos;
    }

    public override void Initialize()
    {
        gridsize = this.size.x * this.size.y;
        grid = new Cell[size.x,size.y];
        rooms = new List<Room>();

        for (int j = 0; j < size.x; j++)
        {
            for (int k = 0; k < size.y; k++)
            {
                rooms.Add(new Room(new Vector2Int(j,k),Room.roomType.None));
            }
        }
        // currentCell = startPos;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // sensor.AddObservation(size);
        for (int i = 0; i < rooms.Count; i++)
        {
            sensor.AddOneHotObservation(changetoInt(rooms[i].roomtype), 5);      
            //Debug.Log( );
        }
    }

    int changetoInt(Room.roomType type) 
    {
        if (type == Room.roomType.Empty)
        {
            return 0;
        } else if (type == Room.roomType.Enemy)
        {
            return 1;
        } else if (type == Room.roomType.Treasure)
        {
            return 2;
        } else if (type == Room.roomType.Boss)
        {
            return 3;
        } else 
        {
            return 4;
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int roomplacement = actionBuffers.DiscreteActions[0];
        if (roomplacement == 0) 
        {
            // grid[currPos.x,currPos.y].visited = true;
            // Debug.Log(currentCell + " ,");
            rooms[currentCell].roomtype = Room.roomType.Empty;
            currentCell++;
        } else if (roomplacement == 1) 
        {
            //Debug.Log(currPos.x + " ," + currPos.y + " ");
            // grid[currPos.x,currPos.y].visited = true;
            rooms[currentCell].roomtype = Room.roomType.Enemy;
            currentCell++;
        } else if (roomplacement == 2) 
        {
            // grid[currPos.x,currPos.y].visited = true;
            rooms[currentCell].roomtype = Room.roomType.Treasure;
            currentCell++;
        } else if (roomplacement == 3) 
        {
            // grid[currPos.x,currPos.y].visited = true;
            rooms[currentCell].roomtype = Room.roomType.Boss;
            currentCell++;
        } else if (roomplacement == 4) 
        {
            // already none
            // rooms[currentCell].roomtype = Room.roomType.None;
            currentCell++;
        }


        float rewardVal = 0;

        int numEnemies = count(Room.roomType.Enemy);
        if (numEnemies <= 4) 
        {
            // rewardVal += (numEnemies/4);
            rewardVal += 0.25f;
        } else 
        {
            // rewardVal -= ((numEnemies - 4)/4);
            rewardVal += 0.25f;
        }

        int numBoss = count(Room.roomType.Boss);
        if (numBoss == 1) 
        {
            rewardVal += 0.25f;
        } else 
        {
            rewardVal -= 0.25f;
        }

        int numEmpty = count(Room.roomType.Empty);
        if (numEmpty <= 4) 
        {
            // rewardVal += (numEmpty/4);
            rewardVal += 0.25f;
        } else 
        {
            // rewardVal -= ((numEmpty - 4)/4);
            rewardVal -= 0.25f;
        }

        int numTreasure = count(Room.roomType.Treasure);
        if (numTreasure <= 4) 
        {
            // rewardVal += (numTreasure/4);
            rewardVal += 0.25f;
        } else 
        {
            // rewardVal -= ((numTreasure - 4)/4);
            rewardVal -= 0.25f;
        }

        // drawDungeon();

        // Debug.Log(rewardVal);
        SetReward(rewardVal);
        checkEnd();
        // EndEpisode();
    }

    void checkEnd() 
    {
        if (currentCell == 16)  
        {
            EndEpisode();
            AgentReset();
        }
    }

    void AgentReset() 
    {
        for (int j = 0; j < rooms.Count; j++)
        {
            rooms[j].roomtype = Room.roomType.None;
        }
        currentCell = 0;
        Debug.Log("ddddddddddd");
    }

    int count(Room.roomType target) 
    {
        if (rooms.Count == 0) 
        {
             return 0;
        }
        int num = 0;
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].roomtype == target)
            {
                num++;
            }
        }
        return num;
    }

    void drawDungeon() 
    {
        for (int j = 0; j < rooms.Count; j++)
        {
            Room t = rooms[j];
            if (t.roomtype == Room.roomType.None) 
            {
                continue;
            }
            var newRoom = Instantiate(corresRoom(t.roomtype), new Vector3(t.location.x * offset.x, 0, -t.location.y * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
 
            newRoom.name += " " + j;
            Destroy(newRoom);
            Debug.Log(t.roomtype + "" + currentCell);
        }
    }

    GameObject corresRoom(Room.roomType roomType) 
    {
        if (roomType == Room.roomType.Enemy)
        {
            return Enemyroom;
        } else if (roomType == Room.roomType.Boss)
        {
            return Bossroom;
        } else if (roomType == Room.roomType.Treasure)
        {
            return Treasureroom;
        } else
        {
            return Emptyroom;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            discreteActionsOut[0] = Random.Range(0,4);
        }
    }
}
