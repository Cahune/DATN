using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptedSequence2 : MonoBehaviour
{
    public MonoBehaviour dialogue1;
    public MonoBehaviour dialogue2;
    public MonoBehaviour dialogue3;
    public MonoBehaviour dialogue4; // Nghe giảng
    public MonoBehaviour dialogue5; // Nói chuyện bạn

    public GameObject choiceUI; // UI chỉ để hiện hướng dẫn
    public CanvasGroup fadeCanvasGroup; // UI panel đen phủ màn hình, alpha = 0 ban đầu

    private int cupsDrank = 0;
    private bool hasDrankAtLeastOne = false;
    private bool hasEnteredZone = false;

    private bool choiceMade = false;
    private int choiceIndex = -1; // 0 = nghe giảng, 1 = nói chuyện
    public string sceneName = "Scene 2-2";

    void Start()
    {
        StartCoroutine(FullSequence());
    }

    public void OnCupDrank()
    {
        cupsDrank++;
        hasDrankAtLeastOne = true;
        Debug.Log($"Đã uống {cupsDrank} cốc nước.");
    }

    public void OnZoneEntered()
    {
        hasEnteredZone = true;
        Debug.Log("Người chơi đã vào khu vực chỉ định.");
    }

    IEnumerator FullSequence()
    {
        yield return RunEvent(dialogue1, "Dialogue 1");

        yield return new WaitUntil(() => hasDrankAtLeastOne);
        yield return RunEvent(dialogue2, "Dialogue 2");

        hasEnteredZone = false;
        yield return new WaitUntil(() => hasEnteredZone);
        yield return RunEvent(dialogue3, "Dialogue 3");

        Debug.Log("Chờ người chơi nhấn phím 1 hoặc 2...");
        if (choiceUI != null)
            choiceUI.SetActive(true);

        Time.timeScale = 0f; // Dừng game
        yield return StartCoroutine(WaitForKeyPress());
        Time.timeScale = 1f;

        if (choiceUI != null)
            choiceUI.SetActive(false);

        if (choiceIndex == 0)
        {
            Debug.Log("Người chơi chọn: Nghe giảng");
            yield return RunEvent(dialogue4, "Dialogue 4 (Nghe giảng)");
        }
        else if (choiceIndex == 1)
        {
            Debug.Log("Người chơi chọn: Nói chuyện bạn");
            yield return RunEvent(dialogue5, "Dialogue 5 (Nói chuyện bạn)");
        }

        yield return SleepyEffect();
        SceneManager.LoadScene(sceneName);

    }

    IEnumerator WaitForKeyPress()
    {
        while (!choiceMade)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                choiceIndex = 0;
                choiceMade = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                choiceIndex = 1;
                choiceMade = true;
            }

            yield return null;
        }
    }

    IEnumerator RunEvent(MonoBehaviour mb, string name)
    {
        IScriptedEvent ev = mb as IScriptedEvent;
        if (ev != null)
        {
            Debug.Log($"Bắt đầu {name}");
            yield return StartCoroutine(ev.Execute());
            Debug.Log($"Hoàn thành {name}");
        }
        else
        {
            Debug.LogWarning($"{name} không implement IScriptedEvent");
        }
    }

    IEnumerator SleepyEffect()
    {
        if (fadeCanvasGroup == null)
        {
            Debug.LogWarning("Thiếu fadeCanvasGroup!");
            yield break;
        }

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            Time.timeScale = Mathf.Lerp(1f, 0.2f, t); // Làm chậm lại

            yield return null;
        }

        Time.timeScale = 0.2f; // Dừng gần như hoàn toàn
    }
}
