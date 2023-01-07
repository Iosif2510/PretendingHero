using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWarrior : NPC
{
    public override void Attack(GameObject mon)
    {
        if (isAttack) return;
        
        base.Attack(mon);
    }
}
