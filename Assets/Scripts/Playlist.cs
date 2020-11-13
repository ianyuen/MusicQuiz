using System.Collections.Generic;

[System.Serializable]
public class Playlist
{
    public string id { get; set; }
    public List<Question> questions { get; set; }
    public string playlist { get; set; }
}

public class Choice
{
    public string artist { get; set; }
    public string title { get; set; }
}

public class Song
{
    public string id { get; set; }
    public string title { get; set; }
    public string artist { get; set; }
    public string picture { get; set; }
    public string sample { get; set; }
}

public class Question
{
    public string id { get; set; }
    public int answerIndex { get; set; }
    public List<Choice> choices { get; set; }
    public Song song { get; set; }
}