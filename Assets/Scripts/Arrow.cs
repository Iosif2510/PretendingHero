using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Vector2 dir;
    public float damage;

    public void Set(float speed, Vector2 dir, float damage)
    {
        this.speed = speed;
        this.dir = dir.normalized;
        this.damage = damage;
        
        Debug.Log(this.dir * this.speed + " " + this.speed);

        GetComponent<Rigidbody2D>().velocity = this.dir * this.speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Monster" || col.transform.tag == "Player")
        {
            col.transform.GetComponent<Creature>().Hit(damage, dir.normalized, 1f);
        }
        
        Debug.Log("!!!");
        Destroy(gameObject);
        
    }
}
