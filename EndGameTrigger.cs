using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject endScreen;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
