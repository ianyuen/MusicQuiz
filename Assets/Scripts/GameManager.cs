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
        UserChoices = new List<Choice>();
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

    public int Score { get; set; }
    public string PlaylistID { get; set; }
    public List<Choice> UserChoices { get; set; }
    public List<Playlist> Playlists { get; set; }

    Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
    public void SetTexture(string name, Texture texture)
    {
        textures.Add(name, texture);
    }
    public Texture GetTexture(string name)
    {
        return textures.ContainsKey(name) ? textures[name] : null;
    }

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    public void SetAudioClip(string name, AudioClip audio)
    {
        audioClips.Add(name, audio);
    }
    public AudioClip GetAudioClip(string name)
    {
        return audioClips.ContainsKey(name) ? audioClips[name] : null;
    }
}