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

    public void Reset()
    {
        level = 1;
        maxHp = 100;
        hp = maxHp;
        exp = 0;
        maxExp = 50;
        skillPoint = 2;
        power = 10;
        defense = 10;
        suspicion = 0;
        maxSuspicion = 100;
        eyeNum = 0;
        
        skillLevels = new float[skillInfos.skillCooldown.Length];
        skillLevels[1] = 1;
        skillLevels[3] = 1;
        skillCooldown = new float[skillInfos.skillCooldown.Length];
        
        npcData1._level = 1;
        npcData2._level = 1;
    }

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
        Reset();
    }
}
