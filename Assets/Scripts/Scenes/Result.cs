using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public TMP_Text score;
    public GameObject result;

    float resultPosition = 155;
    Playlist playlist;
    List<Choice> userChoices;

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
        score.text = "Congratulation: " + GameManager.Instance.Score.ToString() + "/" + userChoices.Count.ToString();

        for (int index = 0; index < userChoices.Count; index++)
        {
            Song song = playlist.questions[index].song;
            Choice choice = userChoices[index];
            bool isRight = false;
            if (song.artist == choice.artist && song.title == choice.title)
            {
                isRight = true;
            }
            SpawnResult(song.title, song.picture, isRight);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnResult(string answer, string url, bool isRight)
    {
        Vector3 position = new Vector3(0, resultPosition, 0);
        GameObject newResult = Instantiate(result, position, Quaternion.identity);
        newResult.transform.SetParent(GetComponentInChildren<Image>().transform, false);
        resultPosition -= 60;

        ResultPrefab resultPrefab = newResult.GetComponentInChildren<ResultPrefab>();
        resultPrefab.SetResult(answer, url, isRight);
    }

    public void OnClickReplay()
    {
        GameManager.Instance.Score = 0;
        GameManager.Instance.UserChoices = new List<Choice>();
        SceneManager.LoadScene("Welcome");
    }
}