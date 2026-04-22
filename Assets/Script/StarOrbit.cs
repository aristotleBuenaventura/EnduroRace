using UnityEngine;

public class StarOrbit : MonoBehaviour
{
    public float orbitSpeed = 120f;
    public float radius = 1f; // distance from center
    public GameObject starPrefab;
    public int starCount = 4;

    private GameObject[] stars;

    void Start()
    {
        stars = new GameObject[starCount];

        for (int i = 0; i < starCount; i++)
        {
            float angle = i * Mathf.PI * 2f / starCount;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
            stars[i] = Instantiate(starPrefab, transform.position + pos, Quaternion.identity);
            stars[i].transform.SetParent(transform);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, orbitSpeed * Time.deltaTime, Space.World);
    }
}
