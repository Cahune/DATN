using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PausePanel.activeSelf)
                Continue();
            else
                Pause();
        }

        if (PausePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Phím số 1
            {
                Continue();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // Phím số 2
            {
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) // Phím số 3
            {
            }
        }
    }

    public void Pause()
    {

        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Debug.Log("Resume clicked");

        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

}