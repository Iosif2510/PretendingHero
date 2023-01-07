using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterPlace : MonoBehaviour
{
    private TextMeshProUGUI monsterNameUI, monsterNumberUI;
    private MonsterData monsterData;

    private void Awake()
    {
        monsterNameUI = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        monsterNumberUI = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void SetPlace(MonsterData data)
    {
        monsterData = data;
        monsterNameUI.text = monsterData._name;
        monsterNumberUI.text = MonsterCollectionManager.Instance.CollectedMonsters[monsterData].ToString();
    }
}
