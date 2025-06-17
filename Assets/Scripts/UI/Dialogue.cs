using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public AudioClip[] voiceClips;
    public float textSpeed = 0.05f;
    public float autoNextDelay = 1f;

    private int index;
    private AudioSource audioSource;
    private Coroutine typingCoroutine;
    private Action onComplete;

    public void StartDialogue(Action onDone)
    {
        onComplete = onDone;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        index = 0;
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        PlayVoice(index);
        typingCoroutine = StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (textComponent.text == lines[index])
        //    {
        //        NextLine();
        //    }
        //    else
        //    {
        //        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        //        //audioSource.Stop();
        //        textComponent.text = lines[index];
        //        StartCoroutine(AutoNextAfterClick());
        //    }
        //}
    }

    IEnumerator TypeLine()
    {
        textComponent.text = string.Empty;

        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(autoNextDelay);
        NextLine();
    }

    //IEnumerator AutoNextAfterClick()
    //{
    //    yield return new WaitForSeconds(autoNextDelay);
    //    NextLine();
    //}

    void NextLine()
    {
        //audioSource.Stop();

        if (index < lines.Length - 1)
        {
            index++;
            PlayVoice(index);
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            onComplete?.Invoke();
        }
    }

    void PlayVoice(int i)
    {
        if (voiceClips != null && i < voiceClips.Length && voiceClips[i] != null)
        {
            audioSource.clip = voiceClips[i];
            audioSource.Play();
        }
    }
}
