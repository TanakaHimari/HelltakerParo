using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StoryManager : MonoBehaviour
{
    [SerializeField]

    public InputAction nextAction;

    private TMP_Text dialogueText;

    [SerializeField] private StoryData[] storyDatas;
    [SerializeField] private Image background;
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private TextMeshProUGUI characterName;

    // 選択肢ボタン
    [SerializeField] private Button choiceButton1;
    [SerializeField] private Button choiceButton2;
    [SerializeField] private TextMeshProUGUI choiceText1;
    [SerializeField] private TextMeshProUGUI choiceText2;
    private ColorBlock defaultColor1;
    private ColorBlock defaultColor2;




    [SerializeField]
    private string sceneName = "Scene";

    //ストーリーのエレメント配列番号が必要なのでプロパティを
    public int storyIndex { get; private set; }
    public int textIndex { get; private set; }
    //Startで呼び出そう
    public void Start()
    {
        SetStoryElement(storyIndex, textIndex);

        // 起動時に初期色を保存
        defaultColor1 = choiceButton1.colors;
        defaultColor2 = choiceButton2.colors;

    }

    void ResetChoiceButtonColors()
    {
        choiceButton1.colors = defaultColor1;
        choiceButton2.colors = defaultColor2;
    }

    //呼び出しメソッド
    public void SetStoryElement(int _storyIndex, int _textIndex)
    {

        ResetChoiceButtonColors();
        //同じ言葉をまとめておくためのvar
        var storyElement = storyDatas[_storyIndex].stories[_textIndex];



        //どのストーリーデータの、どのバックグランドか
        background.sprite = storyElement.Background;
        //どのストーリーデータの、どのキャラクタか
        characterImage.sprite = storyElement.CharacterImage;
        //どのストーリーデータの、どのテキストか
        storyText.text = storyElement.StoryText;
        //どのストーリーデータの、どのキャラ名か
        characterName.text = storyElement.CharacterName;
        // 選択肢がある場合のみ表示
        if (!string.IsNullOrEmpty(storyElement.Choice1) || !string.IsNullOrEmpty(storyElement.Choice2))
        {
            choiceButton1.gameObject.SetActive(true);
            choiceButton2.gameObject.SetActive(true);

            choiceText1.text = storyElement.Choice1;
            choiceText2.text = storyElement.Choice2;

            // ボタンにイベントを登録
            choiceButton1.onClick.RemoveAllListeners();
            choiceButton2.onClick.RemoveAllListeners();

            choiceButton1.onClick.AddListener(() => OnChoiceSelected(1));
            choiceButton2.onClick.AddListener(() => OnChoiceSelected(2));
        }
        else
        {
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);

        }
    }

    private void OnChoiceSelected(int choice)
    {
        var storyElement = storyDatas[storyIndex].stories[textIndex];

        storyElement.isUsed = true;

        if (choice == 1)
        {
            // シーン名が設定されていれば遷移
            if (!string.IsNullOrEmpty(storyElement.SceneForChoice1))
            {
                SceneManager.LoadScene(storyElement.SceneForChoice1);
                return;
            }
            else
            {
                // シーン名が空なら通常のストーリー進行
                textIndex = storyElement.NextIndexForChoice1;
            }
        }
        else if (choice == 2)
        {
            if (!string.IsNullOrEmpty(storyElement.SceneForChoice2))
            {
                SceneManager.LoadScene(storyElement.SceneForChoice2);
                return;
            }
            else
            {
                textIndex = storyElement.NextIndexForChoice2;
            }
        }



    SetStoryElement(storyIndex, textIndex);
    }
    
   



    private void OnEnable()
    {
        nextAction.Enable();
    }

    private void OnDisable()
    {
        nextAction.Disable();
    }

    void Update()
    {
        // Spaceキーが押された瞬間を検出
        if (nextAction.WasPerformedThisFrame())
        {
            // インデックスを増やす
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
                    SceneManager.LoadScene(sceneName);
                    return;
                }
            }
            // テキスト部を初期化して次の文章を表示
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