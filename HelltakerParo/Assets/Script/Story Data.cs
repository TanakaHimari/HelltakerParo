using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//ScriptableObjectを作成する
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
    public string CharacterName;
    [TextArea] public string StoryText;

    // プレイヤー選択肢（2択）
    public string Choice1;
    public string Choice2;

    // 選択肢ごとの分岐先インデックス
    public int NextIndexForChoice1;
    public int NextIndexForChoice2;

    // ★追加：選択肢ごとのシーン名
    public string SceneForChoice1;
    public string SceneForChoice2;

    // ★追加：使用済みフラグ
    public bool isUsed = false;


}
