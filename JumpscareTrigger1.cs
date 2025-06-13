using UnityEngine;
using UnityEngine.UI;

public class JumpscareTrigger : MonoBehaviour
{
    public Image jumpscareImage;       // UI Image für den Jumpscare
    public float showDuration = 2f;   // Dauer, wie lange das Bild angezeigt wird

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowJumpscare());
            GetComponent<Collider>().enabled = false; // Nur Collider deaktivieren, nicht das ganze Objekt
        }
    }

    private System.Collections.IEnumerator ShowJumpscare()
    {
        jumpscareImage.gameObject.SetActive(true);       // Bild sichtbar machen
        yield return new WaitForSeconds(showDuration);    // Warten
        jumpscareImage.gameObject.SetActive(false);      // Bild wieder ausblenden
    }
}
