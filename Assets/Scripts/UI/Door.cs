using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator door;
    public GameObject openText;

    public AudioSource doorSound;

    public bool inReach;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inReach = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            DoorOpens();
        }
        else
        {
            DoorCloses();
        }
    }

    void DoorOpens()
    {
        Debug.Log("open");
        door.SetBool("open", true); 
        door.SetBool("close", false);
        doorSound.Play();
    }

    void DoorCloses()
    {
        Debug.Log("close");
        door.SetBool("Open", false);
        door.SetBool("Close", true);
        doorSound.Play();
    }
}
