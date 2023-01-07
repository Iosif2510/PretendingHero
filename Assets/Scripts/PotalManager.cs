using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalManager : MonoBehaviour
{
    private List<Potal> portalList;
    private int lockNum;

    // Start is called before the first frame update
    void Awake()
    {
        portalList = new List<Potal>();
        foreach (Transform child in transform)
        {
            portalList.Add(child.GetComponent<Potal>());
        }
    }
    void Start()
    {
       lockNum = GameManager.Instance.DungeonUnlockNumber;
       Debug.Log(lockNum);

       for(int i=0; i < lockNum; i++)
       {
            portalList[i].gameObject.SetActive(true);
            Debug.Log(i);
       }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
