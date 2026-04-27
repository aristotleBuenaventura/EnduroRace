using UnityEngine;
using UnityEngine.Splines;

[ExecuteAlways]
[RequireComponent(typeof(SplineContainer))]
public class SplineExtrudeCollider : MonoBehaviour
{
    public SplineContainer splineContainer;

    [Header("Collider Settings")]
    public float spacing = 1.5f;
    public Vector3 colliderSize = new Vector3(1f, 2f, 1f);
    public Vector3 centerOffset = Vector3.zero;
    public bool showPreview = true;

    [ContextMenu("Generate Colliders")]
    public void GenerateColliders()
    {
        if (splineContainer == null)
            splineContainer = GetComponent<SplineContainer>();

        // Clear old colliders
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        var spline = splineContainer.Spline;
        float length = spline.GetLength();
        int count = Mathf.Max(2, Mathf.CeilToInt(length / spacing));

        for (int i = 0; i < count; i++)
        {
            float t = i / (float)(count - 1);

            Vector3 pos = splineContainer.EvaluatePosition(t);
            Vector3 tan = splineContainer.EvaluateTangent(t);

            if (tan == Vector3.zero) continue;

            Quaternion rot = Quaternion.LookRotation(tan.normalized, Vector3.up);

            GameObject colObj = new GameObject("WallCol_" + i);
            colObj.transform.SetParent(transform);
            colObj.transform.position = pos + rot * centerOffset;
            colObj.transform.rotation = rot;

            // Box Collider
            BoxCollider col = colObj.AddComponent<BoxCollider>();
            col.size = colliderSize;
            col.isTrigger = true;

            // ✅ IMPORTANT FIX (THIS MAKES TRIGGERS WORK RELIABLY)
            Rigidbody rb = colObj.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (!showPreview || splineContainer == null) return;

        Gizmos.color = Color.green;

        var spline = splineContainer.Spline;
        float length = spline.GetLength();
        int count = Mathf.Max(2, Mathf.CeilToInt(length / spacing));

        for (int i = 0; i < count; i++)
        {
            float t = i / (float)(count - 1);

            Vector3 pos = splineContainer.EvaluatePosition(t);
            Vector3 tan = splineContainer.EvaluateTangent(t);

            if (tan == Vector3.zero) continue;

            Quaternion rot = Quaternion.LookRotation(tan.normalized, Vector3.up);

            Matrix4x4 matrix = Matrix4x4.TRS(
                pos + rot * centerOffset,
                rot,
                Vector3.one
            );

            Gizmos.matrix = matrix;
            Gizmos.DrawWireCube(Vector3.zero, colliderSize);
        }

        Gizmos.matrix = Matrix4x4.identity;
    }
}