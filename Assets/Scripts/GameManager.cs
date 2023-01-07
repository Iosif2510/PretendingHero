using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManager : UnitySingleton<GameManager>
{
    private DayPhase currentPhase;
    public int Day => dayNum;
    private int dayNum;

    public int DungeonUnlockNumber => dungeonUnlockNumber;
    private int dungeonUnlockNumber;  // number of unlocked dungeon
    //public int DungeonUnlockNumber => dungeonUnlockNumber;

    public int DayNum { get { return dayNum; } }
    public DayPhase CurrentPhase { get { return currentPhase; }}


    // Start is called before the first frame update
    void Awake()
    {
        dayNum = 1;
        dungeonUnlockNumber = 1;
        // UIManager.Instance.UpdateDay();
        
    }

 
    private void Update () 
    {
        
    }

    public void NextDay() 
    {
        dayNum++;
        // UIManager.Instance.UpdateDay();
        Debug.Log($"Day {dayNum}");
    }

    public void MovePhase(DayPhase phase) 
    {
        currentPhase = phase;
        Debug.Log(phase);
    }

    public void MovePhase() 
    {
        DayPhase phase = (DayPhase)(((int)currentPhase + 1) % 4);
        MovePhase(phase);
        // Debug.Log(phase);
    }


}
