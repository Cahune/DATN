using System.Collections;
using UnityEngine;

public class DialogueEvent : MonoBehaviour, IScriptedEvent
{
    public Dialogue dialogue;

    public IEnumerator Execute()
    {
        bool isDone = false;
        dialogue.StartDialogue(() => isDone = true);
        yield return new WaitUntil(() => isDone);
    }
}
