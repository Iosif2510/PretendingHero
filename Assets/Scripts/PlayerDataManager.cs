using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : UnitySingleton<PlayerDataManager>
{
    [SerializeField] public NPCData npcData1, npcData2;

    [SerializeField] public PlayerSkillInfos skillInfos;

    [SerializeField]
    public float hp;
    public float maxHp;
    [SerializeField]
    public float exp, maxExp;
    [SerializeField]
    public int level;
    [SerializeField]
    public int skillPoint;

    public int power;
    public int defense;

    [SerializeField] 
    public float suspicion;
    public float maxSuspicion;

    public int eyeNum;
    
    public float[] skillLevels;
    public float[] skillCooldown;

    private void Awake()
    {
        /*
        * 0 : Dash
        * 1 : Rescue -> interaction 
        * 2 : Guard
        * 3 : RemoveTrap -> interaction
        * 4 : Concentration
        * 5 : Transparent
        */
        skillLevels = new float[skillInfos.skillCooldown.Length];
        skillLevels[1] = 1;
        skillLevels[3] = 1;
        skillCooldown = new float[skillInfos.skillCooldown.Length];
    }
}
