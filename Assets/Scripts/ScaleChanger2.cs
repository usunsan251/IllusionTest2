using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

public class ScaleChanger2 : MonoBehaviour
{
    // Y軸操作用のステップ量
    public float scaleStep = 1.0f;
    public float baseScale = 10.0f;

    private string filePath;
    private float startTime;
    private bool isConfirmed = false;

    void Start()
    {
        startTime = Time.time;

        // 1つ目と混ざらないよう、ファイル名を「ScaleLog2.csv」にします
        filePath = Application.dataPath + "/ScaleLog2.csv";

        // 新規作成（Time, ScaleY, Ratio, Event）
        File.WriteAllText(filePath, "Time(s),ScaleY,Ratio,Event\n");

        SaveToCSV("InitialState_Y");
        Debug.Log("ScaleChanger2 ログ開始: " + filePath);
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        Vector3 currentScale = transform.localScale;

        // 上下矢印キーでY軸を操作するように設定
        if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            currentScale.y += scaleStep;
            transform.localScale = currentScale;
            SaveToCSV("Increase");
        }

        if (keyboard.downArrowKey.wasPressedThisFrame)
        {
            currentScale.y -= scaleStep;
            transform.localScale = currentScale;
            SaveToCSV("Decrease");
        }

        // Enterキーで決定
        if (keyboard.enterKey.wasPressedThisFrame || keyboard.numpadEnterKey.wasPressedThisFrame)
        {
            isConfirmed = true;
            SaveToCSV("CONFIRMED_Y");
            Debug.Log($"★ScaleChanger2 確定: {transform.localScale.y}");
        }
    }

    void SaveToCSV(string eventName)
    {
        float elapsedTime = Time.time - startTime;
        float currentY = transform.localScale.y;
        float ratio = currentY / baseScale;

        string line = $"{elapsedTime:F2},{currentY:F2},{ratio:F2},{eventName}\n";
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
            float confirmedRatio = transform.localScale.y / baseScale;
            // 倍率の表示を追加
            GUI.Label(new Rect(20, 80, 800, 50), $"【決定】 スケール:{transform.localScale.y:F2} ({confirmedRatio:F2}倍)", style);
        }
        else
        {
            // 画面中央付近に指示を表示
            GUI.Label(new Rect(20, 80, 800, 50), "Enterキーで確定...", style);
        }
    }
}