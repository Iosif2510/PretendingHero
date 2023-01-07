using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum DayPhase 
    {
        Morning,
        Day,
        BossFight,
        Night,
    }

    public enum MapStage
    {
        SecretGarden = 0,
        MorningScene = 1,
        DungeonA = 2,
        DungeonB = 3,
        DungeonC = 4,
        DungeonD = 5,
        DungeonE = 6,
        BossBattleScene = 7,
        GameOverScene = 8,
    }
}