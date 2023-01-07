using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject deleteText;
    public void NewGame()
    {
        if (SaveLoadManager.Instance.CheckSaveFileExist())
        {
            Debug.Log("Warning : Are you sure you want to delete your data?");
            deleteButton.SetActive(true);
            deleteText.SetActive(true);
        }
        else
        {
            NewGameExe();
        }
    }

    public void NewGameExe()
    {
        SaveLoadManager.Instance.NewGameExe();
    }
    public void LoadGame()
    {
        SaveLoadManager.Instance.LoadGame();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
