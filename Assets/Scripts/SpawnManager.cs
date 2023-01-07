using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int stageLevel;
    
    //public Transform[] spawnPoints;
    //public float createTime = 3f;
    //public float curtime; 

    //private bool enableSpawn = false;
    private int MaxMonster;
    private int monsterCount;              // 현재 몬스터 수

    //찍어낼 게임 오브젝트
    public List<GameObject> monsters = new List<GameObject>();
    public GameObject bossmonster;
    private GameObject monster;

    void Start()
    {
        MaxMonster = 4*stageLevel + 3;       // Max 몬스터 수
        InvokeRepeating("spawnMonster", 1, 2); //1초후 부터, 2초마다 반복해서 실행
    
    }

    public void StopSpawn()
    {
        CancelInvoke("spawnMonster");
    }

       
    void spawnMonster()
    {
        float randomX = Random.Range(-22.0f, 16.0f);    //적이 나타날 X좌표 랜덤 생성
        float randomY = Random.Range(-15.0f, 17.0f);   //적이 나타날 Y좌표 랜덤 생성

        int randomnum = Random.Range(0,3);  //랜덤한 몬스터 번호

        if (monsterCount < MaxMonster)
        {
            monster = (GameObject)Instantiate(monsters[randomnum], new Vector3(randomX, randomY, 0f), Quaternion.identity); //몬스터 생성
            monster.transform.SetParent(this.transform);
            //monsterCount += 1;
        }

        monsterCount = transform.childCount;
        Debug.Log(monsterCount);

    }
    

    public void createBoss()
    {
        //bossmonster.gameObject.SetActive(true);

        float randomX = Random.Range(-26.0f, 17.0f);    //적이 나타날 X좌표 랜덤 생성
        float randomY = Random.Range(-15.0f, 20.0f);   //적이 나타날 Y좌표 랜덤 생성
        GameObject boss = (GameObject)Instantiate(bossmonster, new Vector3(randomX, randomY, 0f), Quaternion.identity);
    }

}
