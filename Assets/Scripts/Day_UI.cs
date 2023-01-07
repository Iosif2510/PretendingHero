using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Day_UI : MonoBehaviour
{
    private TextMeshProUGUI textvariable;

    // Start is called before the first frame update
    void Start()
    {
        textvariable = GetComponent<TextMeshProUGUI>();
        textvariable.text = GameManager.Instance.Day.ToString();
    }

    // Update is called once per frame
    void Update()
    {
         textvariable.text = "Day : " + GameManager.Instance.Day.ToString();
    }
}
