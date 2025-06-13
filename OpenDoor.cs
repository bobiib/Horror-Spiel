using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animation hingehere;
    public Component doorcolliderhere;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.E))
        {
            hingehere.Play();
            doorcolliderhere.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
