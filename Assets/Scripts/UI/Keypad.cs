using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadOB;
    public GameObject hud;
    public GameObject animateOB;
    public Animator ANI;
    public Text textOB;
    public string answer = "122114";
    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;
    public bool animate;

    private bool isPaused = false;

    void Start()
    {
        keypadOB.SetActive(false);
    }

    public void Number(int number)
    {
        textOB.text += number.ToString();
        button.Play();
    }

    public void Execute()
    {
        if (textOB.text == answer)
        {
            correct.Play();
            textOB.text = "Right";
        }
        else
        {
            wrong.Play();
            textOB.text = "Wrong";
        }
    }

    public void Clear()
    {
        textOB.text = "";
        button.Play();
    }

    public void Exit()
    {
        keypadOB.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    void Update()
    {
        if (textOB.text == "Right" && animate)
        {
            ANI.SetBool("animate", true);
            Debug.Log("its open");
        }

        if (keypadOB.activeInHierarchy && !isPaused)
        {
            Time.timeScale = 0f;
            hud.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        }
        else if (!keypadOB.activeInHierarchy && isPaused)
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isPaused = false;
        }
    }
}
