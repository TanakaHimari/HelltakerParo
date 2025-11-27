using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField] private StoryData[] storyDatas;
    [SerializeField] private Image background;
    //[SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI characterName;
    //ストーリーのエレメント配列番号が必要なのでプロパティを
    public int storyIndex { get; private set; }
    public int textIndex { get; private set; }
    //Startで呼び出そう
    public void Start()
    {
        SetStoryElement(storyIndex, textIndex);
    }
    //呼び出しメソッド
    public void SetStoryElement(int _storyIndex, int _textIndex)
    {
        StopAllCoroutines(); // ← 前の文章が残ってたら止める

        //同じ言葉をまとめておくためのvar
        var storyElement = storyDatas[_storyIndex].stories[_textIndex];

        //どのストーリーデータの、どのバックグランドか
        background.sprite = storyElement.Background;
        //どのストーリーデータの、どのキャラクタか
        //characterImage.sprite = storyElement.CharacterImage;

        // 先輩の名前を取得（未入力なら「センパイ」）
        string senpaiName = PlayerPrefs.GetString("SenpaiName", "センパイ");

        // StoryTextとCharacterNameに {senpai} を使って差し込む
        string replacedText = storyElement.StoryText.Replace("{senpai}", senpaiName);
        string replacedName = storyElement.CharacterName.Replace("{senpai}", senpaiName);

        //どのストーリーデータの、どのキャラ名か
        characterName.text = replacedName;

        // 1文字ずつ表示するコルーチンを開始
        StartCoroutine(TypeSentence(replacedText));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //インデックスを増やす
            //テキスト部を初期化して
            textIndex++;

            // 現在の storyData の文章が終わったら
            if (textIndex >= storyDatas[storyIndex].stories.Count)
            {
                // 次の StoryData に進む
                storyIndex++;
                textIndex = 0;

                // すべての StoryData が終わったら本編へ
                if (storyIndex >= storyDatas.Length)
                {
                    SceneManager.LoadScene("InGame");
                    return;
                }
            }

            storyText.text = "";
            characterName.text = "";
            SetStoryElement(storyIndex, textIndex);
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        //１文字づつ文字を分割した状態にする
        // ← 初期化してから表示開始
        storyText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            //1文字表示
            storyText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
}