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

    [Header("Runtime")]
    public bool generateOnStart = true;
    public bool useTrigger = true;
    public bool useParentLayer = false;
    public int wallColliderLayer = 0;

    private bool generatedAtRuntime;

    private void Start()
    {
        if (!Application.isPlaying || !generateOnStart || generatedAtRuntime) return;
        GenerateColliders();
        generatedAtRuntime = true;
    }

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
            colObj.layer = useParentLayer ? gameObject.layer : wallColliderLayer;

            // Box Collider
            BoxCollider col = colObj.AddComponent<BoxCollider>();
            col.size = colliderSize;
            col.isTrigger = useTrigger;

            // Keep generated wall colliders stable for trigger/collision events.
            Rigidbody rb = colObj.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            // Combine spline collider generation + wall bump behavior.
            wallBumpMesh wallBump = colObj.AddComponent<wallBumpMesh>();
            wallBump.tripStaminaCost = tripStaminaCost;
            wallBump.stunDuration = stunDuration;
            wallBump.knockbackDistance = knockbackDistance;
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