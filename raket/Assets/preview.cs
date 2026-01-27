using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowPreview : MonoBehaviour
{
    [Header("Input")]
    public KeyCode holdKey = KeyCode.Mouse0;

    [Header("Throw Settings")]
    public Transform throwOrigin;
    public float throwForce = 15f;
    public float gravity = -9.81f;

    [Header("Preview")]
    public LineRenderer line;
    public GameObject impactMarker;
    public int resolution = 30;
    public LayerMask collisionMask;

    void Start()
    {
        line.positionCount = 0;
        impactMarker.SetActive(false);
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            DrawTrajectory();
        }
        else
        {
            line.positionCount = 0;
            impactMarker.SetActive(false);
        }
    }

    void DrawTrajectory()
    {
        Vector3 startPos = throwOrigin.position;
        Vector3 startVel = throwOrigin.forward * throwForce;

        float timestep = 0.1f;

        line.positionCount = resolution;

        Vector3 prevPoint = startPos;

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timestep;

            Vector3 point =
                startPos +
                startVel * t +
                0.5f * Physics.gravity * t * t;

            line.SetPosition(i, point);

            if (i > 0)
            {
                if (Physics.Raycast(prevPoint,
                        point - prevPoint,
                        out RaycastHit hit,
                        Vector3.Distance(prevPoint, point),
                        collisionMask))
                {
                    line.positionCount = i + 1;
                    line.SetPosition(i, hit.point);

                    impactMarker.transform.position = hit.point;
                    impactMarker.SetActive(true);

                    return;
                }
            }

            prevPoint = point;
        }

        impactMarker.SetActive(false);
    }
}
