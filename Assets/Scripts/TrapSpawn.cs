using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public int trapNum;  //1개에 대해
    public GameObject trapObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Transform[trapNum];
        int i = 0;
        foreach (Transform child in transform)
        {
            spawnPoints[i++] = child;
        }
        for (i=0; i<trapNum; i++)
        {
            Instantiate(trapObject, spawnPoints[i]);
        }
    }
}
