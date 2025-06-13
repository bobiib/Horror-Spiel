using UnityEngine;

public class PickKey2 : MonoBehaviour
{
    public Component doorcollider2here;
    public GameObject keygone2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.E))
        {
            doorcollider2here.GetComponent<BoxCollider>().enabled = true;
            keygone2.SetActive(false);
        }
    }
}
