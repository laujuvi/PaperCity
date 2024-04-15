using UnityEngine;

public enum Emotions { None, Neutral, Angry, Talking, Thinking };

public class MessageData
{
    public string Name { get; }
    public Color Color { get; }
    public string Message { get; }
    public Emotions Emotion { get; }

    public MessageData(string name, Color color, string message, Emotions emotion)
    {
        Name = name;
        Color = color;
        Message = message;
        Emotion = emotion;
    }
}
