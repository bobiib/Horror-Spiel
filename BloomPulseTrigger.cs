using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections;

public class BloomPulseTrigger : MonoBehaviour
{
    public PostProcessVolume volume;
    public float maxIntensity = 70f;
    public float duration = 1f;

    private Bloom bloom;
    private bool isPulsing = false;

    private void Start()
    {
        if (!volume.profile.TryGetSettings(out bloom))
        {
            Debug.LogError("Bloom wurde nicht gefunden!");
        }
        else
        {
            bloom.intensity.value = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPulsing)
        {
            isPulsing = true;
            StartCoroutine(PulseBloom());
        }
    }

    private IEnumerator PulseBloom()
    {
        while (true)
        {
            yield return StartCoroutine(LerpBloom(0f, maxIntensity, duration));
            yield return StartCoroutine(LerpBloom(maxIntensity, 0f, duration));
        }
    }

    private IEnumerator LerpBloom(float from, float to, float time)
    {
        float elapsed = 0f;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;
            bloom.intensity.value = Mathf.Lerp(from, to, t);
            yield return null;
        }

        bloom.intensity.value = to;
    }
}
