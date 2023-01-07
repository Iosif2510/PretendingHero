using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class UIManager : UnitySingleton<UIManager>
{
    // Prototype
    // private TextMeshProUGUI dayText;

    private void Awake() 
    {
        // if (GameManager.Instance.CurrentPhase == DayPhase.Morning) 
        // {
        //     dayText = GameObject.Find("DayText").GetComponent<TextMeshProUGUI>();
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneLoaded() 
    {
        // if (GameManager.Instance.CurrentPhase == DayPhase.Morning) 
        // {
        //     dayText = GameObject.Find("DayText").GetComponent<TextMeshProUGUI>();
        // }
        // Debug.Log(dayText);
    }

    //prototype UI
    // public void UpdateDay() {
        // dayText.text = $"Day {GameManager.Instance.DayNum}";
    // }
}
