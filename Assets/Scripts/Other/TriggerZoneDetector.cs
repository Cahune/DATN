using UnityEngine;

public class TriggerZoneDetector : MonoBehaviour
{
    private ScriptedSequence2 sequence;

    void Start()
    {
        sequence = FindFirstObjectByType<ScriptedSequence2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sequence?.OnZoneEntered();
            // Tùy bạn: có thể Destroy(gameObject); nếu chỉ dùng 1 lần
        }
    }
}
