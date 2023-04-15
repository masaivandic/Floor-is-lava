using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPosition : MonoBehaviour
{
    public GameObject Land;
    public GameObject[] spawnPoints;

    public void Update()
    {
        RockPositions();
    }

    public void RockPositions()
    {
        if(Land.transform.position.y < -5.85f)
        {
            int randNum = Random.Range(0, 5);
            transform.position = spawnPoints[randNum].transform.position;
        }
    }

}
