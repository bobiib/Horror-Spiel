using UnityEngine;

public class OpenDoor2 : MonoBehaviour
{
    public Animation hinge2here;
    public GameObject doorcollider2here;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        if(Input.GetKey(KeyCode.E))
        {
            hinge2here.Play();
            doorcollider2here.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
