using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light lightSource;
    public float minIntensity = 10f;
    public float maxIntensity = 20f;
    public float flickerSpeed = 4f;

    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0);
        lightSource.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}