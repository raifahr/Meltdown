using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAccelerate : MonoBehaviour
{
    [Header("Force Settings")]
    public float maxAccelerationForce = 30.0f;
    public float chargeTimeToMax = 1.5f;

    [Header("Mouse Settings")]
    public Camera mainCamera;
    public float raycastPlaneY = 0.0f;

    [Header("Effects")]
    public ParticleSystem accelerationTrail;

    private float holdTime = 0.0f;
    private Rigidbody rb;
    private PlayerStamina stamina;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stamina = GetComponent<PlayerStamina>();

        if (mainCamera == null)
            mainCamera = Camera.main;

        if (accelerationTrail != null)
            accelerationTrail.Stop();
    }

    void Update()
    {
        bool isHolding = Input.GetMouseButton(0) && stamina.HasStamina();

        if (isHolding)
        {
            holdTime += Time.deltaTime;
            stamina.DrainStamina(); 
        }
        else
        {
            holdTime = 0f;
            stamina.RegenerateStamina(); 
        }

        if (accelerationTrail != null)
        {
            if (isHolding)
            {
                if (!accelerationTrail.isPlaying)
                    accelerationTrail.Play();
            }
            else
            {
                if (accelerationTrail.isPlaying)
                    accelerationTrail.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && stamina.HasStamina())
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0.0f, raycastPlaneY, 0.0f));

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

