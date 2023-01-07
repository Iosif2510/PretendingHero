using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int stageLevel;
    public Transform[] spawnPoints;
    public float createTime = 3f;
    //public float curtime; 

    //찍어낼 게임 오브젝트
    public List<GameObject> monster = new List<GameObject>();
    public GameObject bossmonster;

    void Start()
    {
           
        StartCoroutine(this.CreateMonster());
        //curtime += Time.deltaTime;
    
    }
       
    IEnumerator CreateMonster()
    {
        while (true)
        {
            for (int i = 0; i < spawnPoints.Length-1; i++)
            {
                int j = Random.Range(0, monster.Count);
                Instantiate(monster[j], spawnPoints[i]);
            }
            yield return new WaitForSeconds(createTime);
        }

        yield return null;
        
    }

    public void createBoss()
    {
        //bossmonster.gameObject.SetActive(true);
        Instantiate(bossmonster, spawnPoints[spawnPoints.Length-1]);
    }

}
