using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretGardenManager : MonoBehaviour
{
    [SerializeField]
    private Transform monsterPlaceParent;

    private int currentPage;
    private int gardenPlaceNumber = 20;

    // Start is called before the first frame update
    void Awake()
    {
        currentPage = 0;
        SetGarden();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetGarden()
    {
        for (int i = 0; i < gardenPlaceNumber; i++)
        {
            var monsterPlace = monsterPlaceParent.GetChild(i).GetComponent<MonsterPlace>();
            int monsterIndex = currentPage * gardenPlaceNumber + i;
            if (MonsterCollectionManager.Instance.CollectableMonsters.Count > monsterIndex)
            {
                if (MonsterCollectionManager.Instance.GetMonsterNumberByIndex(monsterIndex) > 0)
                {
                    monsterPlace.gameObject.SetActive(true);
                    monsterPlace.SetPlace(MonsterCollectionManager.Instance.CollectableMonsters[monsterIndex]);
                }
                else
                {
                    monsterPlace.gameObject.SetActive(false);
                }
            }
            else
            {
                monsterPlace.gameObject.SetActive(false);
            }
        }
    }

    public void ChangePage(int page)
    {
        currentPage = page;
        SetGarden();
    }

    public void NextPage()
    {
        ChangePage(currentPage + 1);
    }

    public void PriorPage()
    {
        ChangePage(currentPage - 1);
    }
}
