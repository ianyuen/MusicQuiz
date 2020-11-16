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
        StartCoroutine(SetImage(url));
        button.enabled = false;
        button.GetComponentInChildren<TMP_Text>().text = answer;
        if (isRight)
        {
            button.GetComponent<Image>().color = new Color(38/255f, 111/255f, 37/255f);
        }
    }

    IEnumerator SetImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        picture.texture = DownloadHandlerTexture.GetContent(www);
        picture.enabled = true;
    }
}