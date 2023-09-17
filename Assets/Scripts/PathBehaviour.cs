using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehaviour : MonoBehaviour
{
    // pillars associate to the right
    public GameObject[] upwall;
    public GameObject[] downwall;
    public GameObject[] rightwall;
    public GameObject[] leftwall;
    public GameObject[] pillars; // upr,upl,dnr,dnl

    public void UpdateRoom(bool[] status)
    {
        GameObject[][] walls = {upwall,downwall,rightwall,leftwall};

        for (int i = 0; i < status.Length; i++)
        {
            if (status[i] == true)
            {
                walls[i][0].SetActive(!status[i]);
                walls[i][1].SetActive(!status[i]);
                walls[i][2].SetActive(!status[i]);
            }
        }

        if (status[0] == true && status[2] == true)
        {
            pillars[0].SetActive(false);
        }
        
        if (status[0] == true && status[3] == true)
        {
            pillars[1].SetActive(false);
        }

        if (status[1] == true && status[2] == true)
        {
            pillars[2].SetActive(false);
        }

        if (status[1] == true && status[3] == true)
        {
            pillars[3].SetActive(false);
        }                
    }
}
