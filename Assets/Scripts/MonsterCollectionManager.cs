using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollectionManager : UnitySingleton<MonsterCollectionManager>
{
    public static int monsterNumber = 20;

    [SerializeField]
    private List<MonsterData> collectableMonsters;

    // number of each collected monster
    private Dictionary<MonsterData, int> collectedMonsters;

    public List<MonsterData> CollectableMonsters
    {
        get 
        {
            return collectableMonsters;
        }
    }

    public Dictionary<MonsterData, int> CollectedMonsters
    {
        get 
        {
            return collectedMonsters;
        }
    }

    private void Awake() 
    {
        collectedMonsters = new Dictionary<MonsterData, int>();
        foreach (var monster in collectableMonsters)
        {
            collectedMonsters.Add(monster, 0);
        }
        // collectedMonsters[collectableMonsters[0]] = 3;
    }

    public void CollectMonster(MonsterData monster, int number)
    {
        collectedMonsters[monster] = number;
    }

    public void CollectMonster(MonsterData monster)
    {
        collectedMonsters[monster]++;
    }

    public int GetMonsterNumberByIndex(int index)
    {
        return collectedMonsters[collectableMonsters[index]];
    }
}
