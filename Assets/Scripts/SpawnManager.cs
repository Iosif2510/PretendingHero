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
    
    float randomX ;    //적이 나타날 X좌표 랜덤 생성
    float randomY ;   //적이 나타날 Y좌표 랜덤 생성

    public GameObject Monster_ => bossmonster;

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
        
        int randomnum = Random.Range(0,3);  //랜덤한 몬스터 번호
        
        switch(stageLevel)
        {
            case 1 :
                randomX = Random.Range(-22.0f, 16.0f);
                randomY = Random.Range(-12.0f, 19.0f);
                break;

            case 2 :
                randomX = Random.Range(-21.0f, 20.0f);
                randomY = Random.Range(-17.0f, 17.0f);
                break;

            case 3 :
                randomX = Random.Range(-21.0f, 15.0f);
                randomY = Random.Range(-19.0f, 14.0f);
                break;

            case 4 :
                randomX = Random.Range(-19.0f, 19.0f);
                randomY = Random.Range(-21.0f, 13.0f);
                break;

            case 5 :
                randomX = Random.Range(-13.0f, 17.0f);
                randomY = Random.Range(-15.0f, 13.0f);
                break;
        }

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
        switch(stageLevel)
        {
            case 1 :
                randomX = Random.Range(-22.0f, 16.0f);
                randomY = Random.Range(-12.0f, 19.0f);
                break;

            case 2 :
                randomX = Random.Range(-21.0f, 20.0f);
                randomY = Random.Range(-17.0f, 17.0f);
                break;

            case 3 :
                randomX = Random.Range(-21.0f, 15.0f);
                randomY = Random.Range(-19.0f, 14.0f);
                break;

            case 4 :
                randomX = Random.Range(-19.0f, 19.0f);
                randomY = Random.Range(-21.0f, 13.0f);
                break;

            case 5 :
                randomX = Random.Range(-13.0f, 17.0f);
                randomY = Random.Range(-15.0f, 13.0f);
                break;
        }
        
        GameObject boss = (GameObject)Instantiate(bossmonster, new Vector3(randomX, randomY, 0f), Quaternion.identity);
    }

}
