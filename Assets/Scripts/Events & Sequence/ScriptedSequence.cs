using System.Collections;
using UnityEngine;

public class ScriptedSequence : MonoBehaviour
{
    public MonoBehaviour[] eventList; 
    void Start()
    {
        StartCoroutine(RunSequence());
    }

    IEnumerator RunSequence()
    {
        foreach (MonoBehaviour mb in eventList)
        {
            IScriptedEvent ev = mb as IScriptedEvent;
            if (ev != null)
            {
                Debug.Log($"Bắt đầu event: {mb.name}");
                yield return StartCoroutine(ev.Execute());
                Debug.Log($"Hoàn thành event: {mb.name}");
            }
            else
            {
                Debug.LogWarning($"{mb.name} không implement IScriptedEvent");
            }
        }
        Debug.Log(" Tất cả event đã chạy xong.");
    }

}
