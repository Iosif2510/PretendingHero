using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSight : MonoBehaviour
{
    private float _inteval;
    private NPC _npc;

    private void Start()
    {
        _npc = GetComponentInParent<NPC>();
    }

    private void Update()
    {
        _inteval -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerDataManager.Instance.eyeNum++;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (_inteval > 0) return;
        
        if (col.tag == "Monster")
        {
            _inteval = _npc.data._attackDelay;
            GetComponentInParent<NPC>().Attack(col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerDataManager.Instance.eyeNum--;
        }
    }
}
