using UnityEngine;

public class BellRandom : MonoBehaviour
{
    public GameObject[] bells;

    void Start()
    {
        if (bells == null || bells.Length == 0) return;

        int randomIndex = Random.Range(0, bells.Length);

        for (int i = 0; i < bells.Length; i++)
        {
            bells[i].SetActive(i == randomIndex);
        }
    }
}