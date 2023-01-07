using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;
//using SpawnManager;

public class Timer_UI : MonoBehaviour
{
    [SerializeField] SpawnManager theSpawnManager;
    
    private TextMeshProUGUI timer;
    public float LimitTime;
    private bool boss = true;

    
    //public List<GameObject> delmonster = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        timer.text = GameManager.Instance.DayNum.ToString();
        //var delMonsters = new GameObject[3];
        theSpawnManager = GameObject.Find("Monsters").GetComponent<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        LimitTime -= Time.deltaTime;
        timer.text = $"Remain Time\n{Mathf.Round(LimitTime)}";
        
        if(LimitTime <= 0)
        {
            if(boss)
            {
                Debug.Log("보스 타임");
                GameManager.Instance.MovePhase();

                var delMonster = FindObjectsOfType<Monster>();
                for(int i=0; i<delMonster.Length; i++)
                {
                    delMonster[i].gameObject.SetActive(false);
                }

                theSpawnManager.createBoss();
                LimitTime = 10;
                boss = false;
            }

            else
            {
                GameManager.Instance.DungeonUnlockNumber += 1;
                GameSceneManager.Instance.LoadStage((MapStage)1);
            }
            
        }

        
    }


}
