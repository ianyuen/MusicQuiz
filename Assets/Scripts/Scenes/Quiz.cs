using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public GameObject resultButton;
    public List<GameObject> answerButtons;
    public List<RawImage> images;

    Vector2 scrollViewVector = Vector2.zero;

    int index = -1;
    Song song;
    Question question;
    Playlist playlist;

    // Start is called before the first frame update
    void Start()
    {
        resultButton.SetActive(false);
        foreach (RawImage image in images)
        {
            image.enabled = false;
        }

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

    public void NextQuestion()
    {
        if (index < playlist.questions.Count - 1)
        {
            index += 1;
            question = playlist.questions[index];
            for (int i = 0; i < question.choices.Count; i++)
            {
                Choice choice = question.choices[i];
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = choice.title;
            }
            song = question.song;

            StartCoroutine(SetImage(song.picture));
            StartCoroutine(SetAudio(song.sample));
        }
        else
        {
            foreach (GameObject answerButton in answerButtons)
            {
                answerButton.SetActive(false);
            }
            resultButton.SetActive(true);
        }
    }

    void ShowImage(string name)
    {
        foreach (RawImage image in images)
        {
            if (image.name == name)
            {
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }
        }
    }

    public void OnClickAnswer1()
    {
        CheckAnswer(question.choices[0]);
        NextQuestion();
    }

    public void OnClickAnswer2()
    {
        CheckAnswer(question.choices[1]);
        NextQuestion();
    }

    public void OnClickAnswer3()
    {
        CheckAnswer(question.choices[2]);
        NextQuestion();
    }

    public void OnClickAnswer4()
    {
        CheckAnswer(question.choices[3]);
        NextQuestion();
    }

    public void OnClickResult()
    {
        SceneManager.LoadScene("Result");
    }

    void CheckAnswer(Choice choice)
    {
        GameManager.Instance.UserChoices.Add(choice);
        if (song.artist == choice.artist && song.title == choice.title)
        {
            GameManager.Instance.Score += 1;
            ShowImage("trueImage");
        }
        else
        {
            ShowImage("falseImage");
        }
    }

    IEnumerator SetImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        images[0].texture = DownloadHandlerTexture.GetContent(www);
        ShowImage("picture");
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