using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Day_UI : MonoBehaviour
{
    private TextMeshProUGUI textvariable;

    // Start is called before the first frame update
    void Awake()
    {
        textvariable = GetComponent<TextMeshProUGUI>();
        textvariable.text = GameManager.Instance.DayNum.ToString();
        SceneManager.sceneLoaded += DayUpdate;
    }

    // Update is called once per frame
    public void DayUpdate(Scene scene, LoadSceneMode mode)
    {
        textvariable.text = $"Day {GameManager.Instance.DayNum.ToString()}";
    }
}
