using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[System.Serializable]
public class SaveData
{

    // Player Data
    public float hp, maxHp;
    public float exp, maxExp;
    public int level;
    public int power, defense;

    public int skillPoint;

    public float[] skillLevels;
    public float[] skillCooldown;

    //Game Manager
    public int dayNum;
    public int currentPhase;
    public int dungeonUnlockNumber;

    //Monster Collection
    public List<int> collectedMonstersList;

    public SaveData()
    {
        collectedMonstersList = new List<int>();
        for (int i = 0; i < MonsterCollectionManager.monsterNumber; i++)
        {
            collectedMonstersList.Add(0);
        }
    }

}
