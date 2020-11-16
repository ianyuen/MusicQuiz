using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPrefab : MonoBehaviour
{
    public Button button;

    public void SetResult(string answer, bool isRight)
    {
        button.enabled = false;
        button.GetComponentInChildren<TMP_Text>().text = answer;
        if (isRight)
        {
            button.GetComponent<Image>().color = new Color(38/255f, 111/255f, 37/255f);
        }
    }
}