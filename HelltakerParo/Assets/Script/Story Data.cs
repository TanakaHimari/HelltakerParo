using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//ScriptableObject‚ğì¬‚·‚é
[CreateAssetMenu(fileName = "New Data",menuName ="StoryData")]
public class StoryData : ScriptableObject
{
    public List<Story> stories = new List<Story>();
}

[System.Serializable]

public class Story
{
    public Sprite Background;
    public Sprite CharacterImage;
    [TextArea]
    public string StoryText;
    public string CharacterName;

}

