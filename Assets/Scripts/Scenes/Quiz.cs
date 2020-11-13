using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public new GameObject gameObject;
    Vector2 scrollViewVector = Vector2.zero;

    int index = -1;
    Song song;
    Image[] images;
    Texture texture;
    Playlist playlist;

    void OnGUI()
    {
        if (texture != null)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginVertical("Box");

            ShowImage("true", false);
            ShowImage("false", false);

            GUILayout.Box(texture, GUILayout.Height(Screen.width));

            scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
            foreach (Choice choice in playlist.questions[index].choices)
            {
                if (GUILayout.Button(choice.title))
                {
                    GameManager.Instance.UserChoices.Add(choice);
                    if (song.artist == choice.artist && song.title == choice.title)
                    {
                        GameManager.Instance.Score += 1;
                        ShowImage("true", true);
                    }
                    else
                    {
                        ShowImage("false", true);
                    }
                    NextQuestion();
                }
            }

            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        images = gameObject.GetComponentsInChildren<Image>();

        ShowImage("true", false);
        ShowImage("false", false);

        foreach (Playlist playlist in GameManager.Instance.Playlists)
        {
            if (playlist.id == GameManager.Instance.PlaylistID)
            {
                this.playlist = playlist;
            }
        }

        NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextQuestion()
    {
        if (index < playlist.questions.Count - 1)
        {
            texture = null;

            index += 1;
            song = playlist.questions[index].song;

            StartCoroutine(SetImage(song.picture));
            StartCoroutine(SetAudio(song.sample));
        }
        else
        {
            SceneManager.LoadScene("Result");
        }
    }

    void ShowImage(string name, bool enabled)
    {
        foreach (Image image in images)
        {
            if (image.name == name)
            {
                image.enabled = enabled;
                break;
            }
        }
    }

    IEnumerator SetImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        texture = DownloadHandlerTexture.GetContent(www);
    }

    IEnumerator SetAudio(string url)
    {
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);
        yield return www.SendWebRequest();

        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = DownloadHandlerAudioClip.GetContent(www);
        audio.Play();
    }
}