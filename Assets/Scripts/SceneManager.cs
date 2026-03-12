using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // シーン遷移に必要
using UnityEngine.UI;              // ボタンの制御に必要

public class SceneControl : MonoBehaviour
{
    // インスペクターで作成したボタンをここにドラッグ＆ドロップする
    public GameObject nextButton;

    void Start()
    {
        // 念のため、開始時はボタンを隠しておく
        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        // エンターキーが押されたらボタンを表示
        if (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame)
        {
            ShowNextButton();
        }
    }

    public void ShowNextButton()
    {
        if (nextButton != null)
        {
            nextButton.SetActive(true);
        }
    }

    // ボタンが押された時に実行する関数
    public void OnNextButtonClick()
    {
        // "Scene2" という名前のシーンに移動
        SceneManager.LoadScene("Scene2");
    }
}