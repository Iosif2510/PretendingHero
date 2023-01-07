using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSight : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            GetComponentInParent<NPC>().Attack(col.gameObject);
        }
    }
}
