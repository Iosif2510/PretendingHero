using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   
    public Transform[] spawnPoints;
    public float createTime = 3f;
    //public float curtime; 


    //찍어낼 게임 오브젝트
    public List<GameObject> monster = new List<GameObject>();


    void Start()
    {
           
        StartCoroutine(this.CreateMonster());
        //curtime += Time.deltaTime;
    
    }
       
    IEnumerator CreateMonster()
    {

        for(int i=0; i<5; i++)
        {
            //몬스터의 생성 주기 시간만큼 대기
            yield return new WaitForSeconds(createTime);

            //몬스터의 생성
            Instantiate(monster[i], spawnPoints[i]);
        }

        yield return null;
        
        
    }


    
}
