using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TMP_UI : MonoBehaviour
{
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text skillPoint;
    [SerializeField] private Slider hp;
    [SerializeField] private Slider exp;
    [SerializeField] private Slider suspicion;
    [SerializeField] private MovementController player;

    public float rate;

    private void Update()
    {
        level.text = "Level " + player.level;
        skillPoint.text = "Skill Point :\n" + player.skillPoint;
        hp.value = Mathf.Lerp(hp.value, player.hp / player.maxHp, rate*Time.deltaTime);
        exp.value = Mathf.Lerp(exp.value, player.exp / player.maxExp, rate*Time.deltaTime);
        suspicion.value = Mathf.Lerp(suspicion.value,
            PlayerDataManager.Instance.suspicion / PlayerDataManager.Instance.maxSuspicion,
            rate * Time.deltaTime);
    }

    public void ChangeHp(float value)
    {
        player.hp += value;
    }
    
    public void ChangeExp(float value)
    {
        player.exp += value;
    }

    public void AddSkillLevel(int index)
    {
        player.AddSkillLevel(index);
    }
}
