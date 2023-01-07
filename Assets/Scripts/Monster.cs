using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;

    public int frequency; //몬스터 속도
    public int nextMove; //몬스터 방향

    Rigidbody2D monster;

    public int currentHealth;
    

    private void Awake()
    {
        monster = GetComponent<Rigidbody2D>();
        currentHealth = data._health;

        Invoke("Think", 1);
    }
    
    //충돌 콜라이더
    private void OnCollisionEnter2D(Collision2D collision)
    {
	
        Debug.Log(collision.gameObject.name + " : 충돌 시작");

        if(collision.collider.CompareTag("Left"))
        {
	        nextMove = 2;
        }

        else if(collision.collider.CompareTag("Right"))
        {
	        nextMove = 1;
        }

        else if(collision.collider.CompareTag("Up"))
        {
	        nextMove = 3;
        }

        else if(collision.collider.CompareTag("Down"))
        {
	        nextMove = 4;
        }

        else if(collision.collider.CompareTag("Obstacle"))
        {
            Vector3 yourloc = collision.gameObject.transform.position;
            Vector3 mypos = monster.transform.position;

            Vector2 dirVec = new Vector2(yourloc.x - mypos.x, yourloc.y - mypos.y);
            dirVec = dirVec.normalized * (-2);
            monster.velocity = new Vector2(dirVec.x, dirVec.y);

            Debug.Log("속도값" + monster.velocity);
        }

    }


    void FixedUpdate()
    {
        //기본 Move
        switch(nextMove)
        {
            case 0:
                monster.velocity = new Vector2(monster.velocity.x, monster.velocity.y);
                break;

            case 1:
                monster.velocity = new Vector2(-1, monster.velocity.y);
                break;
            
            case 2:
                monster.velocity = new Vector2(1, monster.velocity.y);
                break;
            
            case 3:
                monster.velocity = new Vector2(monster.velocity.x, -1);
                break;
            
            case 4:
                monster.velocity = new Vector2(monster.velocity.x, 1);
                break;
        }

    }

    

    void Think()
    {
        //멈춤, 왼쪽, 오른쪽, 위, 아래 = 0, 1, 2, 3, 4
        nextMove = Random.Range(0, 5);

        Invoke("Think", 1);
    }

    

}