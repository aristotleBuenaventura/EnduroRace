using UnityEngine;

public class CrowdLineSpawner : MonoBehaviour
{
    [Header("Crowd Models")]
    public GameObject[] crowdPrefabs;   // Crowd 1, Crowd 2 (must have valid Animator)

    [Header("Row Setup")]
    public Transform[] rowSpawnPoints;  // One empty GameObject per stand row
    public int peoplePerRow = 5;
    public float spacing = 1.2f;

    [Header("Variation")]
    public float sidewaysJitter = 0.15f;
    public float forwardJitter = 0.1f;
    public float rotationJitter = 10f;

    void Start()
    {
        SpawnCrowd();
    }

    void SpawnCrowd()
    {
        foreach (Transform row in rowSpawnPoints)
        {
            for (int i = 0; i < peoplePerRow; i++)
            {
                // Pick random crowd model
                GameObject prefab = crowdPrefabs[Random.Range(0, crowdPrefabs.Length)];

                // Position along the row (LOCAL RIGHT, not world X)
                Vector3 position =
                    row.position +
                    row.right * i * spacing +
                    row.right * Random.Range(-sidewaysJitter, sidewaysJitter) +
                    row.forward * Random.Range(-forwardJitter, forwardJitter);

                // Rotation follows the row
                Quaternion rotation =
                    row.rotation *
                    Quaternion.Euler(0, Random.Range(-rotationJitter, rotationJitter), 0);

                GameObject crowd = Instantiate(prefab, position, rotation, transform);

                SetupAnimator(crowd);
            }
        }
    }

    void SetupAnimator(GameObject crowd)
    {
        Animator anim = crowd.GetComponent<Animator>();
        if (anim == null) return;

        // Random cheering state
        bool isCheering = Random.value > 0.5f;
        anim.SetBool("IsCheering", isCheering);

        // Randomize animation start time so they are not in sync
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.shortNameHash, 0, Random.Range(0f, 1f));
    }
}
