using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using static Define;

public class SaveLoadManager : UnitySingleton<SaveLoadManager>
{
    private SaveData mySaveData;
    private string saveDataName = "SaveData.json";
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject deleteText;

    private void Awake()
    {
        mySaveData = new SaveData();
    }

    private void SaveDataSync()
    {
        mySaveData.hp = PlayerDataManager.Instance.hp;
        mySaveData.maxHp = PlayerDataManager.Instance.maxHp;
        mySaveData.exp = PlayerDataManager.Instance.exp;
        mySaveData.maxExp = PlayerDataManager.Instance.maxExp;
        mySaveData.level = PlayerDataManager.Instance.level;

        mySaveData.skillPoint = PlayerDataManager.Instance.skillPoint;
        mySaveData.skillLevels = PlayerDataManager.Instance.skillLevels;
        
        mySaveData.dayNum = GameManager.Instance.DayNum;
        mySaveData.currentPhase = (int)GameManager.Instance.CurrentPhase;
        mySaveData.dungeonUnlockNumber = GameManager.Instance.DungeonUnlockNumber;

        for (int i = 0; i < MonsterCollectionManager.Instance.CollectableMonsters.Count; i++)
        {
            mySaveData.collectedMonstersList[i] = MonsterCollectionManager.Instance.GetMonsterNumberByIndex(i);
            Debug.Log("ASDGSDG " + mySaveData.collectedMonstersList[i]);
        }
    }

    private void LoadDataSync()
    {
        PlayerDataManager.Instance.hp = mySaveData.hp;
        PlayerDataManager.Instance.maxHp = mySaveData.maxHp;
        PlayerDataManager.Instance.exp = mySaveData.exp;
        PlayerDataManager.Instance.maxExp = mySaveData.maxExp;
        PlayerDataManager.Instance.level = mySaveData.level;

        PlayerDataManager.Instance.skillPoint = mySaveData.skillPoint;
        PlayerDataManager.Instance.skillLevels = mySaveData.skillLevels;
        
        GameManager.Instance.SetDay(mySaveData.dayNum);
        GameManager.Instance.MovePhase((DayPhase)mySaveData.currentPhase);
        GameManager.Instance.DungeonUnlockNumber = mySaveData.dungeonUnlockNumber;

        for (int i = 0; i < MonsterCollectionManager.Instance.CollectableMonsters.Count; i++)
        {
            var monster = MonsterCollectionManager.Instance.CollectableMonsters[i];
            MonsterCollectionManager.Instance.CollectMonster(monster, mySaveData.collectedMonstersList[i]);
        }

        switch ((DayPhase)mySaveData.currentPhase)
        {
            case DayPhase.Morning:
                GameSceneManager.Instance.LoadStage(MapStage.MorningScene);
                break;
            case DayPhase.Night:
                GameSceneManager.Instance.LoadStage(MapStage.SecretGarden);
                break;
        }
    }

    public void Save()
    {
        SaveDataSync();
        string dataString = JsonConvert.SerializeObject(mySaveData);
        string filePath = $"{Application.persistentDataPath}/{saveDataName}";
        File.WriteAllText(filePath, dataString);
        Debug.Log("Data Saved.");
    }

    public bool Load()
    {
        if (!CheckSaveFileExist())
        {
            Debug.Log("Data Absent.");
            return false;
        }
        string filePath = $"{Application.persistentDataPath}/{saveDataName}";
        string loadedString = File.ReadAllText(filePath);
        mySaveData = JsonConvert.DeserializeObject<SaveData>(loadedString);
        Debug.Log("Data Loaded.");
        LoadDataSync();
        return true;
    }

    public bool CheckSaveFileExist()
    {
        string filePath = $"{Application.persistentDataPath}/{saveDataName}";
        return File.Exists(filePath);
    }

    public void NewGameExe()
    {
        mySaveData = new SaveData();
        GameManager.Instance.MovePhase(DayPhase.Morning);
        GameSceneManager.Instance.LoadStage(MapStage.MorningScene);
        SceneManager.LoadScene("PrologueScene");
    }

    public void LoadGame()
    {
        if (Load()) GameSceneManager.Instance.LoadStage(MapStage.SecretGarden);
    }
}
