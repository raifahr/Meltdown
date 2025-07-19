using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float forceMultiplier = 10.0f;
    public Camera mainCamera;
    public float raycastPlaneY = 0.0f;
    public float rotationSpeed = 10.0f; // How fast the ship rotates
    public float MinDistance = 20.0f; // How fast the ship rotates
    public float MaxDistance = 100.0f; // How fast the ship rotates

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0.0f, raycastPlaneY, 0.0f));

        if (plane.Raycast(ray, out float distance))
        {
            if (distance < MinDistance || distance > MaxDistance)
            {
                return;
            }

            Vector3 mouseWorldPos = ray.GetPoint(distance);
            Vector3 DistanceScalar = mouseWorldPos - rb.position;
            DistanceScalar.y = 0.0f;
            float MoveDistance = DistanceScalar.magnitude;
            Vector3 direction = (DistanceScalar).normalized;

            rb.AddForce(direction * forceMultiplier);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
