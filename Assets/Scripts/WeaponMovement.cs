using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponMovement : MonoBehaviour
{
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
        
        float angle = Random.Range(0f, Mathf.PI*2);
        Vector2 jitter = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * jitterStrength;
        transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition) + (Vector3)jitter;
    }
}
