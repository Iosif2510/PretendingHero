using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public GameObject[] text;
    private int idx;

    private void Awake()
    {
        idx = 0;
    }

    public void Next()
    {
        text[idx].gameObject.SetActive(false);
        idx++;
        if (idx == 7)
        {
            SceneManager.LoadScene("MorningScene");
        }
        else
        {
            text[idx].gameObject.SetActive(true);
        }
    }
}
