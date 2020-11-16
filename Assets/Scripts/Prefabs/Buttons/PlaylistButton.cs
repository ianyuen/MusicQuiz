﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaylistButton : MonoBehaviour
{
    public Button button;

    string playlistID;
    string playlistName;
    public void SetPlaylist(string playlistID, string playlistName)
    {
        this.playlistID = playlistID;
        this.playlistName = playlistName;
        button.GetComponentInChildren<TMP_Text>().text = this.playlistName;
    }
    public void OnClick()
    {
        GameManager.Instance.PlaylistID = playlistID;
        SceneManager.LoadScene("Quiz");
    }
}