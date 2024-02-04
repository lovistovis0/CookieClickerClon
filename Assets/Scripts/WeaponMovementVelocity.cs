using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponMovementVelocity : MonoBehaviour
{
    [SerializeField] private float acceleration = 0.005f;
    [SerializeField] private float maxVelocity = 0.05f;
    [SerializeField] private float velocityDropoff = 0.9f;
    [SerializeField] private float jitterStrength = 0.1f;

    private Camera mainCamera;
    private bool cameraIsNull;
    private Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            Debug.LogWarning("No camera found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraIsNull)
        {
            return;
        }
        
        Vector2 point = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        velocity += (point - (Vector2)transform.position) * acceleration;
        velocity = Vector2.ClampMagnitude(velocity, maxVelocity);
        velocity *= velocityDropoff;

        float angle = Random.Range(0f, Mathf.PI*2);
        Vector2 jitter = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * jitterStrength;

        float deltaValue = 60 * Time.deltaTime;
        transform.position += (Vector3)velocity * deltaValue + (Vector3)jitter * deltaValue;
    }
}
