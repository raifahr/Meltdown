using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    public GameObject flamethrower;
    public ParticleSystem flameParticles;

    private Collider flameCollider;
    private PlayerStamina stamina;

    void Start()
    {
        stamina = GetComponent<PlayerStamina>();

        if (flamethrower != null)
        {
            flameCollider = flamethrower.GetComponent<Collider>();
            flameCollider.enabled = false;
        }

        if (flameParticles != null)
        {
            flameParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void Update()
    {
        bool isHolding = Input.GetMouseButton(1);

        if (isHolding)
        {
            stamina.DrainStamina();
        }
        else
        {
            stamina.RegenerateStamina();
        }

        bool canUseFlamethrower = isHolding && stamina.HasStamina();

        // Activate flamethrower
        if (canUseFlamethrower)
        {
            if (flameCollider != null && !flameCollider.enabled)
            {
                flameCollider.enabled = true;
                Debug.Log("Collider enabled");
            }

            if (flameParticles != null && !flameParticles.isPlaying)
            {
                flameParticles.Play();
                Debug.Log("Flame particles playing");
            }
        }
        // Deactivate flamethrower
        else
        {
            if (flameCollider != null && flameCollider.enabled)
            {
                flameCollider.enabled = false;
                Debug.Log("Collider disabled");
            }

            if (flameParticles != null && flameParticles.isPlaying)
            {
                flameParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                Debug.Log("Flame particles stopped");
            }
        }
    }
}
