using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static Define;

public class ToMain : MonoBehaviour
{
    private void Start()
    {
        SetReason();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void SetReason()
    {
        TextMeshProUGUI deathReason = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        switch (GameManager.Instance.deathReason)
        {
            case Death.DidntDie:
                deathReason.text = "You Never Died.\nThis must be an Error.";
                break;
            case Death.NoHealth:
                deathReason.text = "You Died Due to Low Health.\nRest in Monsters."
                break;
            case Death.Suspicion:
                deathReason.text = "You are Banished Due to Others' Suspicion.\nFarewell, Society!."
                break;
            case Death.NoHealth:
                deathReason.text = "You Failed to Protect Unique Monster.\nYou Had One Job."
                break;
        }
            

    }
}
