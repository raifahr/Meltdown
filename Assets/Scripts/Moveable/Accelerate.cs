using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Accelerate : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 3.0f;
    public float staminaDrainRate = 1.0f;
    public float staminaRegenRate = 0.5f;
    private float currentStamina;

    [Header("Force Settings")]
    public float maxAccelerationForce = 30.0f;
    public float chargeTimeToMax = 1.5f;

    [Header("Mouse Settings")]
    public Camera mainCamera;
    public float raycastPlaneY = 0f;

    private float holdTime = 0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        bool isHolding = Input.GetMouseButton(0) && currentStamina > 0f;

        if (isHolding)
        {
            holdTime += Time.deltaTime;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else
        {
            holdTime = 0f;
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && currentStamina > 0f)
        {
            // Get mouse world direction
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0f, raycastPlaneY, 0f));

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPos = ray.GetPoint(distance);
                Vector3 toTarget = mouseWorldPos - rb.position;
                toTarget.y = 0f;

                Vector3 direction = toTarget.normalized;
                float chargeRatio = Mathf.Clamp01(holdTime / chargeTimeToMax);
                float force = chargeRatio * maxAccelerationForce;

                rb.AddForce(direction * force);
            }
        }
    }
}
