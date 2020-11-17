using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{
    public Image background;
    public GameObject button;

    int buttonStart = -80;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.Playlists.Count; i++)
        {
            Playlist playlist = GameManager.Instance.Playlists[i];
            SpawnButton(playlist.id, playlist.playlist);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextScene(string playlistID)
    {
        GameManager.Instance.PlaylistID = playlistID;
        SceneManager.LoadScene("Quiz");
    }

    void SpawnButton(string playlistID, string playlistName)
    {
        Vector3 position = new Vector3(0, buttonStart, 0);
        GameObject newButton = Instantiate(button, position, Quaternion.identity);
        newButton.transform.SetParent(background.transform, false);
        buttonStart += 80;

        PlaylistButton playlistButton = newButton.GetComponentInChildren<PlaylistButton>();
        playlistButton.SetPlaylist(playlistID, playlistName);
        playlistButton.onClick = NextScene;
    }
}