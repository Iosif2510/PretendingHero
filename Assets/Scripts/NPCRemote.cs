using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRemote : NPC
{
    [SerializeField] private GameObject arrow;
    public override void Attack(GameObject mon)
    {
        if (isAttack) return;
        
        base.Attack(mon);
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(0.5f);
        var ar = Instantiate(arrow, transform.position + (Vector3)dir*1.5f, transform.rotation);
        ar.transform.SetParent(transform);

        var a = ar.GetComponent<Arrow>();
        a.Set(data._power + data._level * data._powerUp, dir, data._power);
    }
}
