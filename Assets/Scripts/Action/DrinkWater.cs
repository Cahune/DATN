using UnityEngine;

public class DrinkWater : MonoBehaviour
{
    public GameObject cup;
    public GameObject drinkText;

    public bool inReach;

    private ScriptedSequence2 sequence;

    void Start()
    {
        inReach = false;
        sequence = FindFirstObjectByType<ScriptedSequence2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            drinkText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            drinkText.SetActive(false);

        }
    }




    void Update()
    {
        if (Input.GetButtonDown("Interact") && inReach)
        {
            cup.SetActive(false);
            drinkText.SetActive(false);

            if (sequence != null)
                sequence.OnCupDrank();
        }


    }
}
