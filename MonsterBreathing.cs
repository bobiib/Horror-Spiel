using UnityEngine;

public class MonsterBreathing : MonoBehaviour
{
    public Transform chestBone;
    public float amplitude = 0.005f;
    public float frequency = 0.5f;

    private Vector3 initialPosition;

    void Start()
    {
        if (chestBone == null)
        {
            Debug.LogWarning("Chest bone not assigned!");
            enabled = false;
            return;
        }

        initialPosition = chestBone.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency * Mathf.PI * 2f) * amplitude;
        chestBone.localPosition = initialPosition + new Vector3(0f, offset, 0f);
    }
}
