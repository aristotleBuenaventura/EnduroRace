using UnityEngine;

public class WhirlpoolSpin : MonoBehaviour
{
    [Tooltip("Degrees per second")]
    public float spinSpeed = 60f;

    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
    }
}
