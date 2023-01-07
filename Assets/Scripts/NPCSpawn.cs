using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    private Transform[] spawnPoints;
    public int NPCnum;  //1개에 대해
    public List<GameObject> Npc = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Transform[NPCnum * 2];
        int i = 0;
        foreach (Transform child in transform)
        {
            spawnPoints[i++] = child;
        }
        for (i=0; i<NPCnum; i++)
        {
            Instantiate(Npc[0], spawnPoints[i]);
            Instantiate(Npc[1], spawnPoints[i+NPCnum]);
        }
    }
}
