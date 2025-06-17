using System.Collections;
using UnityEngine;

public class CameraActionEvent : MonoBehaviour, IScriptedEvent
{
    public enum ActionType
    {
        Shake,
        TeleportPlayer,
        Wait,
        Help,
        NotHelp
    }

    public ActionType actionType;

    [Header("Common")]
    public Camera targetCamera;

    [Header("Shake Settings")]
    public float shakeDuration = 1f;
    public float shakeIntensity = 0.2f;

    [Header("Teleport Settings")]
    public Transform teleportTarget;
    public Transform playerTransform;

    [Header("Wait Settings")]
    public float waitDuration = 1f;

    [Header("Help / NotHelp Settings")]
    public float helpDuration = 10f;  
    public float helpDistance = 1f; 
    public float notHelpDistance = 3f;

    public IEnumerator Execute()
    {
        switch (actionType)
        {
            case ActionType.Shake:
                yield return ShakeCamera();
                break;
            case ActionType.TeleportPlayer:
                yield return TeleportPlayer();
                break;
            case ActionType.Wait:
                yield return new WaitForSeconds(waitDuration);
                break;
            case ActionType.Help:
                yield return HelpPlayer();
                break;

            case ActionType.NotHelp:
                yield return NotHelpPlayer();
                break;
        }
    }

    IEnumerator ShakeCamera()
    {
        if (targetCamera == null) yield break;

        Vector3 originalPos = targetCamera.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 offset = Random.insideUnitSphere * shakeIntensity;
            targetCamera.transform.localPosition = new Vector3(offset.x, offset.y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetCamera.transform.localPosition = originalPos;
    }

    IEnumerator TeleportPlayer()
    {
        if (teleportTarget != null && playerTransform != null)
        {
            playerTransform.position = teleportTarget.position;
            playerTransform.rotation = teleportTarget.rotation;
        }
        yield return null;
    }

    IEnumerator HelpPlayer()
    {
        if (playerTransform == null) yield break;

        Vector3 start = playerTransform.position;
        Vector3 end = start + playerTransform.forward * helpDistance;

        float elapsed = 0f;
        while (elapsed < helpDuration)
        {
            playerTransform.position =
                Vector3.Lerp(start, end, elapsed / helpDuration);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        playerTransform.position = end;
    }

    IEnumerator NotHelpPlayer()
    {
        if (playerTransform != null)
        {
            // Kéo giật mạnh về phía sau ngay lập tức
            playerTransform.position -=
                playerTransform.forward * notHelpDistance;
        }
        yield return null;
    }
}

