using UnityEngine;

public class ScrollTexture : MonoBehaviour
{
    public float scrollX = 0.5f;
    public float scrollY = 0f;

    void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        GetComponent<Renderer>().material
            .SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
    }
}