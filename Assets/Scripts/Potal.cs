using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Potal : MonoBehaviour, Interactable
{
    [SerializeField] private int stage;
    [SerializeField] private bool dayPhaseShift;
    private bool to_next = false;

    
    public void Interact()
    {
        if (dayPhaseShift) GameManager.Instance.MovePhase();
        // Debug.Log(GameManager.Instance.CurrentPhase);
        GameSceneManager.Instance.LoadStage((MapStage)stage);
    }
}
