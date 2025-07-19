using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float forceMultiplier = 8.0f;
    public Camera mainCamera;
    public float raycastPlaneY = 0.0f;
    public float rotationSpeed = 25.0f; // How fast the ship rotates
    public float minDistance = 4.0f;    // Too close = don't move
    public float maxDistance = 20.0f;   // Too far = don't move
    public float stopDamping = 0.001f;   // Smoothing factor for boat slowing down to a stop

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 1.5f; 

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        // Check if mouse is within screen bounds
        Vector3 mousePos = Input.mousePosition;
        bool mouseOnScreen = mousePos.x >= 0 && mousePos.x <= Screen.width &&
                             mousePos.y >= 0 && mousePos.y <= Screen.height;

        if (!mouseOnScreen)
        {
            // Stop boat if mouse off screen
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, stopDamping);
            rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, stopDamping);
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        Plane plane = new Plane(Vector3.up, new Vector3(0.0f, raycastPlaneY, 0.0f));

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 mouseWorldPos = ray.GetPoint(distance);
            Vector3 toTarget = mouseWorldPos - rb.position;
            toTarget.y = 0f;

            float distanceToTarget = toTarget.magnitude;

            if (distanceToTarget < minDistance || distanceToTarget > maxDistance)
            {
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, stopDamping);
                rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, stopDamping);
                return;
            }

            Vector3 direction = toTarget.normalized;

            rb.AddForce(direction * forceMultiplier);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}