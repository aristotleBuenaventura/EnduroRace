using UnityEngine;

public class ArrowStepLight : MonoBehaviour
{
    public Material arrowMaterial;  // assign your arrow material
    public float stepTime = 0.3f;   // time per arrow
    private int currentArrow = 0;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stepTime)
        {
            timer = 0f;
            currentArrow++;
            if (currentArrow > 2) currentArrow = 0; // 3 arrows in your PNG
            arrowMaterial.mainTextureOffset = new Vector2(currentArrow / 3f, 0f);
        }
    }
}
