using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWarriorAttack : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Monster")
        {
            other.gameObject.GetComponent<Creature>().Hit(GetComponentInParent<NPC>().data._power,
                (other.transform.position - transform.parent.position).normalized, 1f);

            if (other.gameObject.tag == "Player")
            {
                PlayerDataManager.Instance.suspicion += PlayerDataManager.Instance.eyeNum * 2;
            }
        }
    }
}
