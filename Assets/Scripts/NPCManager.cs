using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCManager : MonoBehaviour
{
    public int frequency; //NPC 속도
    public int nextMove; //NPC 방향

    Rigidbody2D NPC;

    void Awake()
    {
        NPC = GetComponent<Rigidbody2D>();

        Invoke("Think", 1);
    }

   void FixedUpdate()
    {
        switch(nextMove)
        {
            case 0:
                NPC.velocity = new Vector2(NPC.velocity.x, NPC.velocity.y);
                break;

            case 1:
                NPC.velocity = new Vector2(-1, NPC.velocity.y);
                break;
            
            case 2:
                NPC.velocity = new Vector2(1, NPC.velocity.y);
                break;
            
            case 3:
                NPC.velocity = new Vector2(NPC.velocity.x, -1);
                break;
            
            case 4:
                NPC.velocity = new Vector2(NPC.velocity.x, 1);
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

