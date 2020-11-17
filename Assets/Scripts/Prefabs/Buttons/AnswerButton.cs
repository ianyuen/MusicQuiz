using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Button button;
    public Action<int> onClick;

    int index;

    public void SetButton(int index, string title)
    {
        this.index = index;
        button.GetComponentInChildren<TMP_Text>().text = title;
    }

    public void OnClick()
    {
        onClick(index);
    }
}