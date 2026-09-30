using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float XRotation;

    [SerializeField] private Transform PlayerBody;

    public Vector2 LockAxis;

    public float Sensivity = 20f;

    void Start()
    {
        Sensivity = PlayerPrefs.GetFloat("Sensitivity", 20f);

        XRotation = transform.localEulerAngles.x;

        if (XRotation > 180f)
        {
            XRotation -= 360f;
        }
    }

    void Update()
    {
        float lookX = LockAxis.x * Sensivity * Time.deltaTime;
        float lookY = LockAxis.y * Sensivity * Time.deltaTime;

        XRotation -= lookY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);

        if (PlayerBody != null)
        {
            PlayerBody.Rotate(0f, lookX, 0f);
        }
    }
}