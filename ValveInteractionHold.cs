using System.Collections;
using UnityEngine;

public class ValveInteractionHold : MonoBehaviour
{
    public Animation valveAnimation;
    public string valveAnimName = "RotateValve";

    public AudioSource behindYouAudio;

    public float holdDuration = 10f;
    public GameObject targetObject;

    private bool playerInZone = false;
    private float heldTime = 0f;
    private bool valveActivated = false;

    void Update()
    {
        if (playerInZone && !valveActivated && Input.GetKey(KeyCode.E))
        {
            heldTime += Time.deltaTime;

            if (!valveAnimation.isPlaying)
                valveAnimation.Play(valveAnimName);

            if (behindYouAudio != null && !behindYouAudio.isPlaying)
                behindYouAudio.Play();

            if (heldTime >= holdDuration)
                StartCoroutine(ActivateValve());
        }

        if (Input.GetKeyUp(KeyCode.E) && !valveActivated)
        {
            heldTime = 0f;

            if (valveAnimation.isPlaying)
                valveAnimation.Stop();

            if (behindYouAudio != null && behindYouAudio.isPlaying)
                behindYouAudio.Stop();
        }
    }

    private IEnumerator ActivateValve()
    {
        valveActivated = true;

        if (valveAnimation.isPlaying)
            valveAnimation.Stop();

        if (behindYouAudio != null && behindYouAudio.isPlaying)
            behindYouAudio.Stop();

        yield return new WaitForSeconds(0.5f);

        Collider col = targetObject.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }
        else
        {
            targetObject.AddComponent<BoxCollider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            heldTime = 0f;

            if (valveAnimation.isPlaying)
                valveAnimation.Stop();

            if (behindYouAudio != null && behindYouAudio.isPlaying)
                behindYouAudio.Stop();
        }
    }
}