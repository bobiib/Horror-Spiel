using UnityEngine;
using UnityEngine.UI;

public class MonsterTrigger : MonoBehaviour
{
    public Animation monsterAnimation;
    public string animationName = "ScareAnim";

    public Animation doorAnimation;
    public string doorAnimationName = "DoorSecret";

    public GameObject screamerImage;
    public float screamerDuration = 2f;

    public AudioSource audioSource;
    public AudioClip screamerSound;

    public Camera mainCamera;
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.3f;

    public GameObject monsterObject;

    private Vector3 originalCamPos;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (monsterAnimation != null)
                monsterAnimation.Play(animationName);

            if (screamerImage != null)
                screamerImage.SetActive(true);

            if (audioSource != null && screamerSound != null)
                audioSource.PlayOneShot(screamerSound);

            if (mainCamera != null)
            {
                originalCamPos = mainCamera.transform.localPosition;
                InvokeRepeating(nameof(ShakeCamera), 0f, 0.02f);
                Invoke(nameof(StopShaking), shakeDuration);
            }

            if (doorAnimation != null)
            {
                doorAnimation[doorAnimationName].speed = 1f;
                doorAnimation[doorAnimationName].time = 0f;
                doorAnimation.Play(doorAnimationName);
            }

            Invoke(nameof(HideScreamer), screamerDuration);
        }
    }

    void HideScreamer()
    {
        if (screamerImage != null)
            screamerImage.SetActive(false);

        if (monsterObject != null)
            monsterObject.SetActive(false);
    }

    void ShakeCamera()
    {
        if (mainCamera != null)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
            mainCamera.transform.localPosition = originalCamPos + shakeOffset;
        }
    }

    void StopShaking()
    {
        CancelInvoke(nameof(ShakeCamera));
        if (mainCamera != null)
            mainCamera.transform.localPosition = originalCamPos;
    }
}