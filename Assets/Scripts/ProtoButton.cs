using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class ProtoButton : MonoBehaviour
{
    public void LoadScene(int stage) 
    {
        GameManager.Instance.MovePhase();
        // Debug.Log(GameManager.Instance.CurrentPhase);
        GameSceneManager.Instance.LoadStage((MapStage)stage);
    }

    public void NextDay()
    {
        GameManager.Instance.NextDay();
    }
}