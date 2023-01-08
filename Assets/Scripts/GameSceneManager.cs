using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class GameSceneManager : UnitySingleton<GameSceneManager>
{
    [SerializeField]
    private List<string> stageScenes;

    private void Start()
    {
        SceneManager.sceneLoaded += (Scene s, LoadSceneMode m) =>
        {
            if (GameManager.Instance.CurrentPhase == DayPhase.Night)
            {
                SaveLoadManager.Instance.Save();
            }
        };
    }

    public void LoadStage(MapStage stage) 
    {
        string sceneName = stageScenes[(int)stage];
        SceneManager.LoadScene(sceneName);
    }

    // public void LoadStage(int stage) {
    //     LoadStage((MapStage)stage);
    // }
}