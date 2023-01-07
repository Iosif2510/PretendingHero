using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : UnitySingleton<PlayerDataManager>
{
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

    [SerializeField] 
    public float suspicion;
    public float maxSuspicion;
    
    public float[] skillLevels;
    public float[] skillCooldown;

    private void Awake()
    {
        skillLevels = new float[skillInfos.skillCooldown.Length];
        skillCooldown = new float[skillInfos.skillCooldown.Length];
    }
}
