using UnityEngine;

public class wallBump : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("wall bang");
        }
    }
}