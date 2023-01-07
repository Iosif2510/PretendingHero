using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/MonsterData", order = 1)]
public class MonsterData : CharacterData
{
    public int _rareNess;
    public int _respawnTimeBound;
    public int _expGiven;
    public float _probabilityOfOccurrence;
}
