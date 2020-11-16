using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResultPrefab : MonoBehaviour
{
    public Button button;
    public RawImage picture;

    public void SetResult(string answer, string url, bool isRight)
    {
        picture.texture = GameManager.Instance.GetTexture(url);
        button.GetComponentInChildren<TMP_Text>().text = answer;
        if (isRight)
        {
            button.GetComponent<Image>().color = new Color(38/255f, 111/255f, 37/255f);
        }
    }
}