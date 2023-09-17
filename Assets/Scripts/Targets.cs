using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class holding all target variables for learning 
 */
public class Targets: MonoBehaviour 
{
    [HideInInspector] public int minEnemies = 1;
    [HideInInspector] public int maxEnemies = 4;
    [HideInInspector] public int minTreasures = 1;
    [HideInInspector] public int maxTreasures = 3;
    [HideInInspector] public int minEmpty = 1;
    [HideInInspector] public int maxEmpty = 3;
    [HideInInspector] public int minBosses = 1;
    [HideInInspector] public int maxBosses = 2;
}