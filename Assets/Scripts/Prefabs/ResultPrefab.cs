using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPrefab : MonoBehaviour
{
    public TMP_Text number;
    public TMP_Text answer;
    public TMP_Text you;

    public void SetResult(string number, string answer, string you)
    {
        this.number.text = number;
        this.answer.text = answer;
        this.you.text = you;
    }
}