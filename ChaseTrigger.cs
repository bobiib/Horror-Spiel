using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    public GameObject monster;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monster.SetActive(true);
            gameObject.SetActive(false); // Trigger nur einmal nutzbar
        }
    }
}
