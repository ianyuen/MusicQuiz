using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public List<GameObject> answerButtons;
    public List<RawImage> images;
    public RawImage picture;
    public Image blur;

    int index = -1;
    Song song;
    Question question;
    Playlist playlist;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        blur.enabled = false;
        picture.enabled = false;
        SetButtonActive(false);
        foreach (RawImage image in images)
        {
            image.enabled = false;
        }
        foreach (GameObject answerButton in answerButtons)
        {
            answerButton.GetComponentInChildren<AnswerButton>().onClick = OnClickAnswer;
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

    void NextQuestion()
    {
        SetButtonEnable(false);
        if (index < playlist.questions.Count - 1)
        {
            index += 1;
            question = playlist.questions[index];
            song = question.song;

            StartCoroutine(SetImage(song.picture));
            StartCoroutine(SetAudio(song.sample));
        }
        else
        {
            StartCoroutine(WaitAndNextScene());
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

    void SetButtonActive(bool active)
    {
        foreach (GameObject answerButton in answerButtons)
        {
            answerButton.gameObject.SetActive(active);
        }
    }

    void SetButtonEnable(bool enable)
    {
        foreach (GameObject answerButton in answerButtons)
        {
            answerButton.GetComponentInChildren<Button>().enabled = enable;
        }
    }

    void CheckAnswer(Choice choice)
    {
        blur.enabled = true;
        audioSource.Stop();
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

    public void OnClickAnswer(int index)
    {
        CheckAnswer(question.choices[index]);
        NextQuestion();
    }

    IEnumerator SetImage(string url)
    {
        if (GameManager.Instance.GetTexture(url) == null)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();
            GameManager.Instance.SetTexture(url, DownloadHandlerTexture.GetContent(www));
        }
        yield return new WaitForSeconds(0.5f);
        foreach (RawImage image in images)
        {
            image.enabled = false;
        }
        blur.enabled = false;

        picture.texture = GameManager.Instance.GetTexture(url);
        picture.enabled = true;

        for (int i = 0; i < question.choices.Count; i++)
        {
            Choice choice = question.choices[i];
            answerButtons[i].GetComponentInChildren<AnswerButton>().SetButton(i, choice.title);
        }

        SetButtonActive(true);
        SetButtonEnable(true);
    }

    IEnumerator SetAudio(string url)
    {
        if (GameManager.Instance.GetAudioClip(url) == null)
        {
            UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.WAV);
            yield return www.SendWebRequest();
            GameManager.Instance.SetAudioClip(url, DownloadHandlerAudioClip.GetContent(www));
        }
        yield return new WaitForSeconds(0.5f);
        audioSource.clip = GameManager.Instance.GetAudioClip(url);
        audioSource.Play();
    }

    IEnumerator WaitAndNextScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Result");
    }
}