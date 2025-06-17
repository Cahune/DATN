using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Để chuyển scene

public class Keypad2 : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadOB;
    public GameObject hud;
    public Text textOB;
    public string answer = "4869";
    public AudioSource button;
    public AudioSource correct;
    public AudioSource wrong;

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

            // Chuyển sang scene thứ 5 trong Build Settings
            SceneManager.LoadScene("Scene 5");
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    void Update()
    {
        if (keypadOB.activeInHierarchy && !isPaused)
        {
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
