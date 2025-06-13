using UnityEngine;

public class DoorOpen3 : MonoBehaviour
{
    public Animation hinge3here;
    public GameObject doorcollider3here;
    public string doorAnimationName = "DoorSecret";
    private bool isOpen = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !isOpen)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        hinge3here[doorAnimationName].speed = 1f;
        hinge3here[doorAnimationName].time = 0f;
        hinge3here.Play(doorAnimationName);

        doorcollider3here.GetComponent<BoxCollider>().enabled = false;
        isOpen = true;
    }

    public void CloseDoor()
    {
        hinge3here[doorAnimationName].speed = -1f;
        hinge3here[doorAnimationName].time = hinge3here[doorAnimationName].length;
        hinge3here.Play(doorAnimationName);

        isOpen = false;
    }
}