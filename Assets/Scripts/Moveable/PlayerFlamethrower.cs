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
            flameParticles.Stop();
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && stamina.HasStamina())
        {
            if (!flameCollider.enabled)
                flameCollider.enabled = true;

            if (!flameParticles.isPlaying)
                flameParticles.Play();

            stamina.DrainStamina();
        }
        else
        {
            if (flameCollider != null && flameCollider.enabled)
                flameCollider.enabled = false;

            if (flameParticles != null && flameParticles.isPlaying)
                flameParticles.Stop();
            stamina.RegenerateStamina(); 
        }
    }
}

