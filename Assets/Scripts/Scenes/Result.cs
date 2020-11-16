using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    public TMP_Text score;
    public GameObject result;

    float resultPosition = 140.5f;
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
        score.text = "Score: " + GameManager.Instance.Score.ToString() + "/" + userChoices.Count.ToString();

        for (int index = 0; index < userChoices.Count; index++)
        {
            Song song = playlist.questions[index].song;
            Choice choice = userChoices[index];
            SpawnResult(index + 1, song.title, choice.title);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnResult(int number, string answer, string you)
    {
        Vector3 position = new Vector3(0, resultPosition, 0);
        GameObject newResult = Instantiate(result, position, Quaternion.identity);
        newResult.transform.SetParent(transform, false);
        resultPosition -= 50;

        ResultPrefab resultPrefab = newResult.GetComponentInChildren<ResultPrefab>();
        resultPrefab.SetResult(number.ToString(), answer, you);
    }

    public void OnClickReplay()
    {
        SceneManager.LoadScene("Welcome");
    }
}