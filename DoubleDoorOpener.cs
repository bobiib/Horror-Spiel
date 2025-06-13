using UnityEngine;

public class DoubleDoorOpener : MonoBehaviour
{
    public Animation leftDoorAnimation;
    public string leftDoorAnimName = "OpenLeft";

    public Animation rightDoorAnimation;
    public string rightDoorAnimName = "OpenRight";

    public GameObject doorBlockCollider;

    private bool isOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            OpenDoors();
        }
    }

    private void OpenDoors()
    {
        isOpened = true;

        if (leftDoorAnimation != null)
            leftDoorAnimation.Play(leftDoorAnimName);

        if (rightDoorAnimation != null)
            rightDoorAnimation.Play(rightDoorAnimName);

        if (doorBlockCollider != null)
            doorBlockCollider.GetComponent<Collider>().enabled = false;
    }
}