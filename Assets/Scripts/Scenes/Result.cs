using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    Vector2 scrollViewVector = Vector2.zero;

    Playlist playlist;
    List<Choice> userChoices;

    public void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical("Box");

        if (GUILayout.Button("Replay"))
        {
            GameManager.Instance.Score = 0;
            GameManager.Instance.UserChoices = new List<Choice>();
            SceneManager.LoadScene("Welcome");
        }

        GUILayout.Label("Score: " + GameManager.Instance.Score.ToString() + "/" + userChoices.Count.ToString());

        scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
        for (int index = 0; index < userChoices.Count; index++)
        {
            GUILayout.Label("Question: " + (index + 1).ToString());

            Song song = playlist.questions[index].song;
            GUILayout.Label("Answer: " + song.title);

            Choice choice = userChoices[index];
            GUILayout.Label("You: " + choice.title);

            GUILayout.Label("");
        }
        GUILayout.EndScrollView();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Playlist playlist in GameManager.Instance.Playlists)
        {
            if (playlist.id == GameManager.Instance.PlaylistID)
            {
                this.playlist = playlist;
            }
        }

        userChoices = GameManager.Instance.UserChoices;
    }

    // Update is called once per frame
    void Update()
    {

    }
}