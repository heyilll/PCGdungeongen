using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator2D : MonoBehaviour
{
    public class TreeNode
    {
        public int ID;
        public List<int> keys;
        public List<int> children;
        public TreeNode(int node_id) 
        {
            this.ID = node_id;
            keys = new List<int>();
            children = new List<int>();
        }
    }

    public class Room
    {
        public Vector2Int location;
        public roomType roomtype;
        public bool[] status = new bool[4];

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

    public GameObject[] rooms;
    public List<Room> parseddun = new List<Room>();
    public Vector2 offset;
    public int minnodes = 5;
    string fullstring = "dim(1) dim(2) dim(3) dim(4) dim(5) dim(6) dim(7) dim(8) dim(9) tile((1,1)) tile((2,1)) tile((3,1)) tile((4,1)) tile((5,1)) tile((6,1)) tile((7,1)) tile((8,1)) tile((9,1)) tile((1,2)) tile((2,2)) tile((3,2)) tile((4,2)) tile((5,2)) tile((6,2)) tile((7,2)) tile((8,2)) tile((9,2)) tile((1,3)) tile((2,3)) tile((3,3)) tile((4,3)) tile((5,3)) tile((6,3)) tile((7,3)) tile((8,3)) tile((9,3)) tile((1,4)) tile((2,4)) tile((3,4)) tile((4,4)) tile((5,4)) tile((6,4)) tile((7,4)) tile((8,4)) tile((9,4)) tile((1,5)) tile((2,5)) tile((3,5)) tile((4,5)) tile((5,5)) tile((6,5)) tile((7,5)) tile((8,5)) tile((9,5)) tile((1,6)) tile((2,6)) tile((3,6)) tile((4,6)) tile((5,6)) tile((6,6)) tile((7,6)) tile((8,6)) tile((9,6)) tile((1,7)) tile((2,7)) tile((3,7)) tile((4,7)) tile((5,7)) tile((6,7)) tile((7,7)) tile((8,7)) tile((9,7)) tile((1,8)) tile((2,8)) tile((3,8)) tile((4,8)) tile((5,8)) tile((6,8)) tile((7,8)) tile((8,8)) tile((9,8)) tile((1,9)) tile((2,9)) tile((3,9)) tile((4,9)) tile((5,9)) tile((6,9)) tile((7,9)) tile((8,9)) tile((9,9)) start((1,1)) finish((9,9)) sprite((9,9),boss) sprite((1,9),enemy) sprite((6,8),enemy) sprite((7,3),enemy) sprite((5,3),enemy) sprite((2,3),enemy) sprite((1,3),enemy) sprite((7,2),enemy) sprite((6,2),enemy) sprite((5,2),enemy) sprite((3,2),enemy) sprite((7,1),enemy) sprite((7,9),path) sprite((2,9),path) sprite((9,8),path) sprite((7,8),path) sprite((5,8),path) sprite((4,8),treasure) sprite((2,8),path) sprite((1,8),path) sprite((9,7),path) sprite((8,7),treasure) sprite((6,7),treasure) sprite((5,7),path) sprite((4,7),path) sprite((2,7),path) sprite((1,7),path) sprite((9,6),path) sprite((7,6),path) sprite((6,6),path) sprite((4,6),path) sprite((2,6),path) sprite((1,6),path) sprite((9,5),path) sprite((8,5),path) sprite((7,5),path) sprite((6,5),path) sprite((2,5),path) sprite((1,5),path) sprite((9,4),path) sprite((8,4),path) sprite((6,4),path) sprite((5,4),path) sprite((4,4),path) sprite((3,4),path) sprite((2,4),path) sprite((1,4),path) sprite((9,3),path) sprite((8,3),path) sprite((6,3),path) sprite((4,3),path) sprite((3,3),path) sprite((9,2),path) sprite((8,2),path) sprite((4,2),treasure) sprite((2,2),treasure) sprite((1,2),path) sprite((8,1),path) sprite((6,1),path) sprite((5,1),path) sprite((4,1),path) sprite((3,1),path) sprite((2,1),treasure) sprite((1,1),path) adj((1,2),(1,1)) adj((2,1),(1,1)) adj((2,2),(2,1)) adj((3,1),(2,1)) adj((1,1),(2,1)) adj((3,2),(3,1)) adj((4,1),(3,1)) adj((2,1),(3,1)) adj((4,2),(4,1)) adj((5,1),(4,1)) adj((3,1),(4,1)) adj((5,2),(5,1)) adj((6,1),(5,1)) adj((4,1),(5,1)) adj((6,2),(6,1)) adj((7,1),(6,1)) adj((5,1),(6,1)) adj((7,2),(7,1)) adj((8,1),(7,1)) adj((6,1),(7,1)) adj((8,2),(8,1)) adj((7,1),(8,1)) adj((1,3),(1,2)) adj((2,2),(1,2)) adj((1,1),(1,2)) adj((2,3),(2,2)) adj((3,2),(2,2)) adj((1,2),(2,2)) adj((2,1),(2,2)) adj((3,3),(3,2)) adj((4,2),(3,2)) adj((2,2),(3,2)) adj((3,1),(3,2)) adj((4,3),(4,2)) adj((5,2),(4,2)) adj((3,2),(4,2)) adj((4,1),(4,2)) adj((5,3),(5,2)) adj((6,2),(5,2)) adj((4,2),(5,2)) adj((5,1),(5,2)) adj((6,3),(6,2)) adj((7,2),(6,2)) adj((5,2),(6,2)) adj((6,1),(6,2)) adj((7,3),(7,2)) adj((8,2),(7,2)) adj((6,2),(7,2)) adj((7,1),(7,2)) adj((8,3),(8,2)) adj((9,2),(8,2)) adj((7,2),(8,2)) adj((8,1),(8,2)) adj((9,3),(9,2)) adj((8,2),(9,2)) adj((1,4),(1,3)) adj((2,3),(1,3)) adj((1,2),(1,3)) adj((2,4),(2,3)) adj((3,3),(2,3)) adj((1,3),(2,3)) adj((2,2),(2,3)) adj((3,4),(3,3)) adj((4,3),(3,3)) adj((2,3),(3,3)) adj((3,2),(3,3)) adj((4,4),(4,3)) adj((5,3),(4,3)) adj((3,3),(4,3)) adj((4,2),(4,3)) adj((5,4),(5,3)) adj((6,3),(5,3)) adj((4,3),(5,3)) adj((5,2),(5,3)) adj((6,4),(6,3)) adj((7,3),(6,3)) adj((5,3),(6,3)) adj((6,2),(6,3)) adj((8,3),(7,3)) adj((6,3),(7,3)) adj((7,2),(7,3)) adj((8,4),(8,3)) adj((9,3),(8,3)) adj((7,3),(8,3)) adj((8,2),(8,3)) adj((9,4),(9,3)) adj((8,3),(9,3)) adj((9,2),(9,3)) adj((1,5),(1,4)) adj((2,4),(1,4)) adj((1,3),(1,4)) adj((2,5),(2,4)) adj((3,4),(2,4)) adj((1,4),(2,4)) adj((2,3),(2,4)) adj((4,4),(3,4)) adj((2,4),(3,4)) adj((3,3),(3,4)) adj((5,4),(4,4)) adj((3,4),(4,4)) adj((4,3),(4,4)) adj((6,4),(5,4)) adj((4,4),(5,4)) adj((5,3),(5,4)) adj((6,5),(6,4)) adj((5,4),(6,4)) adj((6,3),(6,4)) adj((8,5),(8,4)) adj((9,4),(8,4)) adj((8,3),(8,4)) adj((9,5),(9,4)) adj((8,4),(9,4)) adj((9,3),(9,4)) adj((1,6),(1,5)) adj((2,5),(1,5)) adj((1,4),(1,5)) adj((2,6),(2,5)) adj((1,5),(2,5)) adj((2,4),(2,5)) adj((6,6),(6,5)) adj((7,5),(6,5)) adj((6,4),(6,5)) adj((7,6),(7,5)) adj((8,5),(7,5)) adj((6,5),(7,5)) adj((9,5),(8,5)) adj((7,5),(8,5)) adj((8,4),(8,5)) adj((9,6),(9,5)) adj((8,5),(9,5)) adj((9,4),(9,5)) adj((1,7),(1,6)) adj((2,6),(1,6)) adj((1,5),(1,6)) adj((2,7),(2,6)) adj((1,6),(2,6)) adj((2,5),(2,6)) adj((4,7),(4,6)) adj((6,7),(6,6)) adj((7,6),(6,6)) adj((6,5),(6,6)) adj((6,6),(7,6)) adj((7,5),(7,6)) adj((9,7),(9,6)) adj((9,5),(9,6)) adj((1,8),(1,7)) adj((2,7),(1,7)) adj((1,6),(1,7)) adj((2,8),(2,7)) adj((1,7),(2,7)) adj((2,6),(2,7)) adj((4,8),(4,7)) adj((5,7),(4,7)) adj((4,6),(4,7)) adj((5,8),(5,7)) adj((6,7),(5,7)) adj((4,7),(5,7)) adj((6,8),(6,7)) adj((5,7),(6,7)) adj((6,6),(6,7)) adj((9,7),(8,7)) adj((9,8),(9,7)) adj((8,7),(9,7)) adj((9,6),(9,7)) adj((1,9),(1,8)) adj((2,8),(1,8)) adj((1,7),(1,8)) adj((2,9),(2,8)) adj((1,8),(2,8)) adj((2,7),(2,8)) adj((5,8),(4,8)) adj((4,7),(4,8)) adj((6,8),(5,8)) adj((4,8),(5,8)) adj((5,7),(5,8)) adj((7,8),(6,8)) adj((5,8),(6,8)) adj((6,7),(6,8)) adj((7,9),(7,8)) adj((6,8),(7,8)) adj((9,9),(9,8)) adj((9,7),(9,8)) adj((2,9),(1,9)) adj((1,8),(1,9)) adj((1,9),(2,9)) adj((2,8),(2,9)) adj((7,8),(7,9)) adj((9,8),(9,9))"; 
    List<TreeNode> nodes = new List<TreeNode>();
    public GameObject key;
    int max_keys_per_node = 2;

    public void GenerateDungeon()
    {
        parseDungeon();
        lak();

        for (int i = 0; i < parseddun.Count; i++)
        {
            Room gn = parseddun[i];
            int k = 6;

            if (gn.roomtype == Room.roomType.Treasure) 
            {
                k = 3;
            } else if (gn.roomtype==Room.roomType.Boss)
            {
                k = 2;
            } else if (gn.roomtype==Room.roomType.Empty) 
            {
                var floor = Instantiate(rooms[0], new Vector3(gn.location.x * offset.x, 0, -gn.location.y * offset.y), Quaternion.identity).GetComponent<PathBehaviour>();
                floor.UpdateRoom(gn.status);
                continue;
            } else if (gn.roomtype==Room.roomType.Enemy) 
            {
                k = 1;
            }

            var newRoom = Instantiate(rooms[k], new Vector3(gn.location.x * offset.x, 0, -gn.location.y * offset.y), Quaternion.identity).GetComponent<RoomBehaviour>();
            newRoom.UpdateRoom(gn.status);
            newRoom.name += " " + i + "-";
        }
        PlaceKeysAndLock();
    }

    public void parseDungeon() 
    {
        string[] separatingAdj = { "adj"};
        string[] separatingRooms = { "sprite" };

        int firstDIndex = fullstring.IndexOf("adj");
        int firstSpIndex = fullstring.IndexOf("sp");

        string ebr = fullstring.Substring(firstDIndex);
        string abr = fullstring.Substring(firstSpIndex, firstDIndex - firstSpIndex);

        string[] dors = ebr.Split(separatingAdj, System.StringSplitOptions.RemoveEmptyEntries);
        string[] spr = abr.Split(separatingRooms, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in spr)
        {
            Room.roomType ytr = Room.roomType.None;
            int comma = word.IndexOf(',');
            int be = word.IndexOf(')');
            int bs = word.IndexOf("((");

            string aaa = word.Substring(bs + 2, comma - (bs + 2));
            string bbb = word.Substring(comma+1, be - (comma+1));

            int x = int.Parse(aaa);
            int y = int.Parse(bbb);

            if (word.Contains("treasure")) 
            {
                ytr = Room.roomType.Treasure;
            } else if (word.Contains("boss"))
            {
                ytr = Room.roomType.Boss;
            } else if (word.Contains("path")) 
            {
                ytr = Room.roomType.Empty;
            } else if (word.Contains("enemy")) 
            {
                ytr = Room.roomType.Enemy;
            }

            parseddun.Add(new Room(new Vector2Int(x,y),ytr));
        }
        

        foreach (var dor in dors)
        {
            int bs = dor.IndexOf("((");
            int be = dor.IndexOf(")");
            int firstcomma = dor.IndexOf(',');
            int bt = dor.IndexOf(",(");
            int br = dor.IndexOf("))");
            int lastcomma = dor.LastIndexOf(',');
            string aaa = dor.Substring(bs + 2, firstcomma - (bs + 2));
            string bbb = dor.Substring(firstcomma + 1, be - (firstcomma + 1));
            string ccc = dor.Substring(bt + 2, lastcomma - (bt + 2));
            string ddd = dor.Substring(lastcomma + 1, br - (lastcomma + 1));
            
            int x = int.Parse(aaa);
            int y = int.Parse(bbb);
            int xr = int.Parse(ccc);
            int yr = int.Parse(ddd);

            foreach (Room yun in parseddun)
            {
                if (yun.location == new Vector2Int(x,y)) 
                {                 
                    if (xr - x == -1) 
                    {
                        yun.status[3] = true;
                    }  else if (xr - x == 1)
                    {
                        yun.status[2] = true;
                    } else if (yr - y == -1) 
                    {
                        yun.status[0] = true;
                    } else if (yr - y == 1) 
                    {
                        yun.status[1] = true;
                    }
                    break;
                } 
            }
        }
    }

    // void CheckR(int x, int y, int neighbours)
    // {
    //     Cell but = board[(x + y * size.x)];
    //     but.walls = new bool[4]{true,true,true,true};
    //     int n = 0;
    //     Cell down = board[x + ((y + 1) * size.x)];
    //     Cell right = board[(x + 1) + (y * size.x)];
        

    //     if (y + 1 < size.y && board[x + ((y + 1) * size.x)].visited) 
    //     {
    //             but.walls[1] = false;
    //             down.walls[0] = false;
    //             WallsChanged.Add(down);
    //             Debug.Log("down");
    //             n++;
    //     } 

    //     if (n < neighbours) 
    //     {
    //         if (x + 1 < size.x && board[(x + 1) + (y * size.x)].visited) 
    //         {
    //             but.walls[2] = false;
    //             right.walls[3] = false;
    //             WallsChanged.Add(right);
    //             Debug.Log("right");
    //             n++;
    //         } 
    //     }
        
    //     WallsChanged.Add(but);
    //     return;
    // }

    void lak() 
    {
        // int numkeys = countRooms() - 1;
        int numkeys = Random.Range(minnodes, countRooms() - 1);
        
        for (int i = 0; i < numkeys; i++)
        {
            TreeNode newnode = new TreeNode(i);
            if (i > 0) 
            {
                TreeNode parent = nodes[Random.Range(0,i-1)];
                TreeNode keynode = chooseNode(i);
                parent.children.Add(i);
                keynode.keys.Add(i);
            }
            nodes.Add(newnode);
        }
    }

    TreeNode chooseNode(int i) 
    {
        List<TreeNode> av_nodes = nodes.GetRange(0,i-1);

        for (int j = 0; j < (i-1); j++) 
        {
            int random = Random.Range(0,av_nodes.Count);
            TreeNode candidate = av_nodes[random];
            av_nodes.RemoveAt(random);

            if (candidate.keys.Count < max_keys_per_node) 
            {
                return candidate;
            }
        }
        return nodes[Random.Range(0,i-1)];
    }

    void PlaceKeysAndLock() 
    {
        int l = 0;
        for (int p = 0; p < nodes.Count; p++)
        { 
            TreeNode o = nodes[l];
            Room current = parseddun[p];
            if (!(current.roomtype == Room.roomType.Empty))
            {
                if (o.keys.Count > 0)
                {
                    for (int k = 0; k < o.keys.Count; k++)
                    {
                        int h = o.keys[k];
                        GameObject instKey = Instantiate(key, new Vector3((current.location.x * offset.x), 0, (-(current.location.y) * offset.y) + (k*4)), Quaternion.identity);
                        instKey.name = "Key" + h.ToString();
                    }
                }
                l++;
            }
        }
    }

    int countRooms() 
    {
        int count = 0;
        for (int i = 0; i < parseddun.Count; i++)
        {
            if (!(parseddun[i].roomtype == Room.roomType.Empty) && !(parseddun[i].roomtype == Room.roomType.None)) 
            {
                count++;
            }
        }
        return count;
    }
}
