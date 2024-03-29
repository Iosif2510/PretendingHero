using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class GameManager : UnitySingleton<GameManager>
{
    private DayPhase currentPhase;
    private int dayNum;
    private int dungeonUnlockNumber;  // number of unlocked dungeon
    public Death deathReason;

    public int DungeonUnlockNumber
    {
        get => dungeonUnlockNumber;
        set => dungeonUnlockNumber = value;
    }
    public int DayNum { get { return dayNum; } }
    public DayPhase CurrentPhase { get { return currentPhase; }}

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        deathReason = Death.DidntDie;
        dayNum = 1;
        dungeonUnlockNumber = 1;
        Screen.SetResolution(1920, 1080, true);
        // UIManager.Instance.UpdateDay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDay(int day)
    {
        dayNum = day;
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

    public void GameOver(Death death)
    {
        deathReason = death;
        PlayerDataManager.Instance.Reset();
        SceneManager.LoadScene("GameOverScene");
    }
}
