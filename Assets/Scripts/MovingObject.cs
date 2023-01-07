using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //NPC 움직임

    public float speed;
    public int walkCount;
    protected int currentWalkCount;

    protected Vector3 vector;

    private BoxCollider2D boxCollider;
    public LayerMask LayerMask;
    //private Animator animator;

    protected void Move(string _dir)
    {
        StartCoroutine(MoveCoroutine(_dir));
    }

    //이동이 이루어짐 - direction에 따라 벡터값 조절
    IEnumerator MoveCoroutine(string _dir)
    {
        vector.Set(0, 0, vector.z);

        switch(_dir)
        {
            case "UP" :
                vector.y = 1f;
                break;
            
            case "DOWN" :
                vector.y = -1f;
                break;
            
            case "RIGHT":
                vector.x = 1f;
                break;
            
            case "LEFT":
                vector.x = -1f;
                break;
        }

        //방향 에니메이터
        //animator.SetFloat("DirX", vector.x);
        //animator.SetFloat("DirY", vector.y);
        //animator.SetBool("Walking", true);

        //실제 이동
        while(currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, vector.y * speed, 0);
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }

        currentWalkCount = 0;
        //animator.SetBool("Walking", false);
    }
}
