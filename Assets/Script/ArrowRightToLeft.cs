using UnityEngine;

public class ArrowRightToLeft : MonoBehaviour
{
    public Material arrowMaterial;   // assign your arrow material
    public float stepTime = 0.3f;    // time per arrow
    private int currentArrow = 2;    // start from right-most arrow (index 2)
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= stepTime)
        {
            timer = 0f;
            // Move to next arrow to the left
            currentArrow--;
            if (currentArrow < 0) currentArrow = 2; // loop back to right-most arrow

            // Set UV offset to show current arrow
            arrowMaterial.mainTextureOffset = new Vector2(currentArrow / 3f, 0f);
        }
    }
}
