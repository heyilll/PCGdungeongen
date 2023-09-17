using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] dwalls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;
    public GameObject[] upwall;
    public GameObject[] downwall;
    public GameObject[] leftwall;
    public GameObject[] rightwall;
    public GameObject[] pillars;
    
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            dwalls[i].SetActive(!status[i]);
        }
    }

    public void JoinRooms(bool[] walls) 
    {
        GameObject[][] jjj = {upwall,downwall,rightwall,leftwall};

        for (int i = 0; i < walls.Length; i++) 
        {
            if (walls[i] == false)
            {
                jjj[i][3].SetActive(walls[i]);
                jjj[i][1].SetActive(walls[i]);
                jjj[i][2].SetActive(walls[i]);
                jjj[i][0].SetActive(walls[i]);
            }
        }
    }
}
