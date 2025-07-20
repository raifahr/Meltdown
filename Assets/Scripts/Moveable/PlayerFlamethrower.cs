using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    public GameObject flamethrower;
    public ParticleSystem flameParticles;
    public PlayerStamina playerStamina;

    private Collider flameCollider;

    void Start()
    {
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
        if (Input.GetMouseButton(1) && playerStamina.HasStamina())
        {
            if (!flameCollider.enabled)
                flameCollider.enabled = true;

            if (!flameParticles.isPlaying)
                flameParticles.Play();

            playerStamina.DrainStamina();
        }
        else
        {
            if (flameCollider != null && flameCollider.enabled)
                flameCollider.enabled = false;

            if (flameParticles != null && flameParticles.isPlaying)
                flameParticles.Stop();
        }
    }
}

