using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class ScriptedSequence4 : MonoBehaviour
{
    [Header("Dialogue Events")]
    public MonoBehaviour dialogue1;
    public MonoBehaviour dialogue2;
    public MonoBehaviour dialogue3;

    [Header("Camera Shake Events")]
    public MonoBehaviour help;  
    public MonoBehaviour notHelp;   

    [Header("Choice UI")]
    public GameObject choiceUI;

    private bool choiceMade = false;
    private int choiceIndex = -1;

    private EventSystem eventSystem;

    void Start()
    {
        StartCoroutine(FullSequence());
    }

    IEnumerator FullSequence()
    {
        
        // ---------- Dialogue 1 ----------
        yield return RunEvent(dialogue1, "Dialogue 1");

        // ---------- Hiển thị lựa chọn ----------
        
        if (choiceUI != null) choiceUI.SetActive(true);

        yield return StartCoroutine(WaitForKeyPress());

        if (choiceUI != null) choiceUI.SetActive(false);
        
        

        // ---------- Xử lý theo lựa chọn ----------
        if (choiceIndex == 0)
        {
            yield return RunEvent(dialogue2, "Dialogue 2");
            yield return RunEvent(help, "Help");   
        }
        else if (choiceIndex == 1)
        {
            yield return RunEvent(dialogue3, "Dialogue 3");
            yield return RunEvent(notHelp, "Not Help");   
        }

        Application.Quit();
    }

    IEnumerator WaitForKeyPress()
    {
        while (!choiceMade)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                choiceIndex = 0; choiceMade = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                choiceIndex = 1; choiceMade = true;
            }
            yield return null;
        }
    }

    IEnumerator RunEvent(MonoBehaviour mb, string name)
    {
        if (mb == null)
        {
            Debug.LogWarning($"{name} rỗng"); yield break;
        }

        if (mb is IScriptedEvent ev)
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
}
