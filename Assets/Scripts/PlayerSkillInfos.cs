using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerSkillInfo", order = 1)]
public class PlayerSkillInfos : ScriptableObject
{
    public float[] skillCooldown;
    /*
     * 0 : Dash
     * 1 : Rescue -> interaction 
     * 2 : Guard
     * 3 : RemoveTrap -> interaction
     * 4 : Concentration
     * 5 : Transparent
     */
    public float[] cooldownDecrement;
    public float[] abilityIncrement;
    public float[] maxLevel;
}
