using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            col.GetComponent<Creature>().Hit(10, 
                (Vector2) (col.transform.position -transform.position).normalized,
                1);
        }
    }
}
