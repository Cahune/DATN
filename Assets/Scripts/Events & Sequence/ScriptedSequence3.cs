using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptedSequence3 : MonoBehaviour
{
    public MonoBehaviour[] dialogues; // [0] = Đoạn hội thoại đầu tiên, các phần tử sau là các note
    public string nextSceneName = ""; // Gán nếu muốn chuyển sang cảnh khác sau cùng

    public CanvasGroup fadeCanvasGroup; // Panel đen phủ màn hình để tạo hiệu ứng buồn ngủ

    private bool[] notesRead;
    private int totalNotes;

    void Start()
    {
        if (dialogues.Length < 1)
        {
            Debug.LogError("Cần ít nhất 1 dialogue trong mảng!");
            return;
        }

        totalNotes = dialogues.Length - 1;
        notesRead = new bool[totalNotes];

        StartCoroutine(RunSequence());
    }

    public void OnNoteRead(int noteIndex)
    {
        if (noteIndex >= 1 && noteIndex <= totalNotes)
        {
            notesRead[noteIndex - 1] = true;
            Debug.Log($"Đã đọc note {noteIndex}");
        }
    }

    IEnumerator RunSequence()
    {
        // Chạy đoạn hội thoại đầu tiên
        yield return RunEvent(dialogues[0], "Dialogue 1");

        // Chờ người chơi đọc từng ghi chú
        for (int i = 0; i < totalNotes; i++)
        {
            yield return new WaitUntil(() => notesRead[i]);
            yield return RunEvent(dialogues[i + 1], $"Dialogue {i + 2}");
        }

        Debug.Log("Đã hoàn tất tất cả các đoạn hội thoại theo thứ tự note.");

        // Gọi hiệu ứng buồn ngủ trước khi chuyển cảnh
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            yield return SleepyEffect();
            SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator RunEvent(MonoBehaviour mb, string label)
    {
        IScriptedEvent ev = mb as IScriptedEvent;
        if (ev != null)
        {
            Debug.Log($"Bắt đầu {label}");
            yield return StartCoroutine(ev.Execute());
            Debug.Log($"Kết thúc {label}");
        }
        else
        {
            Debug.LogWarning($"{label} không implement IScriptedEvent");
        }
    }

    IEnumerator SleepyEffect()
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogWarning("Thiếu fadeCanvasGroup! Không thể thực hiện hiệu ứng buồn ngủ.");
            yield break;
        }

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            Time.timeScale = Mathf.Lerp(1f, 0.2f, t); // Làm chậm thời gian

            yield return null;
        }

        Time.timeScale = 0.2f; // Gần như dừng hẳn
    }
}
