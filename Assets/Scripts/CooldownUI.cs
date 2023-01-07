using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [SerializeField] private Sprite skillIconSprite;
    [SerializeField] private Image skillIcon;
    [SerializeField] private Image coolArea;
    [SerializeField] private string key;
    [SerializeField] private int skillIndex;
    [SerializeField] private MovementController movementController;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
        skillIcon.sprite = skillIconSprite;
    }

    private void Update()
    {
        _text.text = key + $" ({movementController.SkillLevels[skillIndex]})";
        if (movementController.SkillLevels[skillIndex] == 0) coolArea.fillAmount = 1;
        else coolArea.fillAmount = movementController.SkillCooldown[skillIndex] 
                                   / movementController.GetCooldown(skillIndex);
    }
}
