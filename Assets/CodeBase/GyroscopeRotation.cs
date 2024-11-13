using TMPro;
using UnityEngine;

public class GyroscopeRotation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;

        float zAngle = Mathf.Atan2(acceleration.x, -acceleration.y) * Mathf.Rad2Deg;

        zAngle = NormalizeAngle(zAngle);

        if (zAngle > 300 && zAngle <= 360)
        {
            zAngle = 0;
        }

        float snappedRotation = SnapToNearest90Degree(zAngle);

        rectTransform.localRotation = Quaternion.Euler(0, 0, snappedRotation);

        if (_text != null)
        {
            _text.text = $"Accelerometer Z Angle: {zAngle:F2}°\nSnapped Rotation (Z): {snappedRotation}°";
        }
    }

    private float SnapToNearest90Degree(float angle)
    {
        float[] possibleAngles = { 0, 90, 180, 270 };
        float closest = possibleAngles[0];
        float minDifference = Mathf.Abs(angle - closest);

        foreach (float possibleAngle in possibleAngles)
        {
            float difference = Mathf.Abs(angle - possibleAngle);
            if (difference < minDifference)
            {
                closest = possibleAngle;
                minDifference = difference;
            }
        }

        return closest;
    }

    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
        return angle;
    }
}
