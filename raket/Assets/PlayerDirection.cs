using UnityEngine;

public class MinimapPlayerArrow : MonoBehaviour
{
    public RectTransform arrow;
    public Transform mainCamera;

    void Update()
    {
        if (arrow == null || mainCamera == null)
            return;

        float yaw = mainCamera.eulerAngles.y;

        arrow.localRotation = Quaternion.Euler(0f, 0f, -yaw);
    }
}
