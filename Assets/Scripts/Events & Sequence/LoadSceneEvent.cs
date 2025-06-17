using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneEvent : MonoBehaviour, IScriptedEvent
{
    public string sceneName;

    public IEnumerator Execute()
    {
        SceneManager.LoadScene(sceneName);
        yield return null;
    }
}
