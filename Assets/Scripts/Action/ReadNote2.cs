using UnityEngine;

public class ReadNote2 : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject pickUpText;
    public AudioSource pickUpSound;
    public bool inReach;


    private CharacterController playerController;
    private ScriptedSequence3 scriptedSequence;

    void Start()
    {
        Time.timeScale = 1f;
        noteUI.SetActive(false);
        hud.SetActive(true);
        pickUpText.SetActive(false);
        inReach = false;

        playerController = player.GetComponent<CharacterController>();
        scriptedSequence = FindFirstObjectByType<ScriptedSequence3>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach )
        {
            //Time.timeScale = 0;
            noteUI.SetActive(true);
            pickUpSound.Play();
            hud.SetActive(false);

            if (playerController != null)
                playerController.enabled = false;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        noteUI.SetActive(false);
        hud.SetActive(true);

        if (playerController != null)
            playerController.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
