using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager instance;
    private GameManager()
    {
        var fileContents = Resources.Load<TextAsset>("coding-test-frontend-unity").text;
        Playlists = JsonConvert.DeserializeObject<List<Playlist>>(fileContents);
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public string PlaylistID { get; set; }
    public List<Playlist> Playlists { get; set; }
}