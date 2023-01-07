using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class Timer_UI : MonoBehaviour
{
    private TextMeshProUGUI timer;
    public float LimitTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        timer.text = GameManager.Instance.DayNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        LimitTime -= Time.deltaTime;
        timer.text = $"Remain Time\n{Mathf.Round(LimitTime)}";

        if(LimitTime <= 0)
        {
            Debug.Log("제한 시간 끝!");
            GameManager.Instance.MovePhase();
            // Debug.Log(GameManager.Instance.CurrentPhase);
            GameSceneManager.Instance.LoadStage((MapStage)0);
        }
    }

}
