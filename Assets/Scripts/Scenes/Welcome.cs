using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    Vector2 scrollViewVector = Vector2.zero;

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical("Box");

        GUILayout.Label("Please choose a playlist");

        scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);

        foreach (Playlist playlist in GameManager.Instance.Playlists)
        {
            if (GUILayout.Button(playlist.playlist))
            {
                GameManager.Instance.PlaylistID = playlist.id;
                SceneManager.LoadScene("Quiz");
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}