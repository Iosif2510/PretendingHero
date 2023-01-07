using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : CharacterData
{
    public int _exp;
    public int _skillPoint;
    public int _suspicionBound;
}
