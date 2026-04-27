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

    [Header("Wall Bump Settings")]
    public float tripStaminaCost = 10f;
    public float stunDuration = 1f;
    public float knockbackDistance = 0.5f;

    [Header("Debug")]
    public bool enableDebugLogs = true;

    [ContextMenu("Generate Colliders")]
    public void GenerateColliders()
    {
        if (splineContainer == null)
            splineContainer = GetComponent<SplineContainer>();

        if (enableDebugLogs)
            Debug.Log($"[SplineExtrudeCollider] GenerateColliders start on {name} | parentLayer={LayerMask.LayerToName(gameObject.layer)}({gameObject.layer})");

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
            colObj.layer = gameObject.layer;

            // Box Collider
            BoxCollider col = colObj.AddComponent<BoxCollider>();
            col.size = colliderSize;
            col.isTrigger = true;

            // Trigger callbacks are more reliable when one side has a Rigidbody.
            Rigidbody rb = colObj.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            // Combine spline collider generation + wall bump behavior.
            wallBumpMesh wallBump = colObj.AddComponent<wallBumpMesh>();
            wallBump.tripStaminaCost = tripStaminaCost;
            wallBump.stunDuration = stunDuration;
            wallBump.knockbackDistance = knockbackDistance;
            wallBump.enableDebugLogs = enableDebugLogs;

            if (enableDebugLogs)
            {
                Debug.Log($"[SplineExtrudeCollider] Created {colObj.name} | pos={colObj.transform.position} | layer={LayerMask.LayerToName(colObj.layer)}({colObj.layer})");
            }
        }

        if (enableDebugLogs)
            Debug.Log($"[SplineExtrudeCollider] GenerateColliders done. Total colliders attempted: {count}");
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