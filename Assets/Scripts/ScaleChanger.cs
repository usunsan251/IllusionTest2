using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class ScaleChanger : MonoBehaviour
{
    public float scaleStep = 1.0f;
    public float baseScale = 10.0f;

    private string filePath;
    private float startTime;
    private bool isConfirmed = false;

    void Start()
    {
        // 実行開始時の時間を記録
        startTime = Time.time;

        // 保存先：Assetsフォルダ内の ScaleLog.csv
        filePath = Application.dataPath + "/ScaleLog.csv";

        // ファイルを新規作成（または上書き）
        File.WriteAllText(filePath, "Time(s),ScaleX,Ratio,Event\n");

        // 最初の状態を記録
        SaveToCSV("InitialState_X");
        Debug.Log("ログ記録開始: " + filePath);
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        Vector3 currentScale = transform.localScale;

        // 右矢印キー
        if (keyboard.rightArrowKey.wasPressedThisFrame)
        {
            currentScale.x += scaleStep;
            transform.localScale = currentScale;
            SaveToCSV("Increase"); // 増加イベント
        }

        // 左矢印キー
        if (keyboard.leftArrowKey.wasPressedThisFrame)
        {
            currentScale.x -= scaleStep;
            transform.localScale = currentScale;
            SaveToCSV("Decrease"); // 減少イベント
        }

        // Enterキーで決定
        if (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame)
        {
            isConfirmed = true;
            SaveToCSV("CONFIRMED_X"); // 確定イベント
            Debug.Log($"★記録完了: {transform.localScale.x}");
        }
    }

    // CSVに現在の状態を追記する関数
    void SaveToCSV(string eventName)
    {
        float elapsedTime = Time.time - startTime;
        float currentX = transform.localScale.x;
        float ratio = currentX / baseScale;

        // 「秒数, スケール, 倍率, イベント内容」
        string line = $"{elapsedTime:F2},{currentX:F2},{ratio:F2},{eventName}\n";

        File.AppendAllText(filePath, line);
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 36;
        style.normal.textColor = Color.white;

        
        if (isConfirmed)
        {
            style.normal.textColor = Color.yellow;
            float confirmedRatio = transform.localScale.x / baseScale;
            // 倍率の表示を追加
            GUI.Label(new Rect(20, 80, 800, 50), $"【決定】 スケール:{transform.localScale.x:F2} ({confirmedRatio:F2}倍)", style);
        }
        else
        {
            // 画面中央付近に指示を表示
            GUI.Label(new Rect(20, 80, 800, 50), "Enterキーで確定...", style);
        }
    }
}