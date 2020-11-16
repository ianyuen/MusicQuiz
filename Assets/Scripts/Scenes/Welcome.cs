using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public GameObject button;

    int buttonSpace = 90;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.Playlists.Count; i++)
        {
            Playlist playlist = GameManager.Instance.Playlists[i];
            SpawnButton(playlist.id, playlist.playlist, new Vector3(0, i * buttonSpace, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnButton(string playlistID, string playlistName, Vector3 position)
    {
        GameObject newButton = Instantiate(button, position, Quaternion.identity);
        newButton.transform.SetParent(transform, false);

        PlaylistButton playlistButton = newButton.GetComponentInChildren<PlaylistButton>();
        playlistButton.SetPlaylist(playlistID, playlistName);
    }
}