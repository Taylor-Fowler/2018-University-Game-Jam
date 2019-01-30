using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float RotationSpeed = 1f;

    private void Update()
    {
        transform.Rotate(0f, 0f, RotationSpeed);
    }
}
