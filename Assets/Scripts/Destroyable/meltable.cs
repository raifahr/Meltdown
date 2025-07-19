using UnityEngine;

public class meltable : MonoBehaviour
{
    public float shrinkRate = 0.8f;
    public float scaleToDestroyAt = 0.2f;
    public ParticleSystem steamParticlePrefab;
    private ParticleSystem steamParticlesInstance;
    private bool isMelting = false;

    void Update()
    {
        if (isMelting)
        {
            if (steamParticlesInstance == null)
            {
                steamParticlesInstance = Instantiate(steamParticlePrefab, transform.position, Quaternion.identity, transform);
                steamParticlesInstance.Play();
            }
            else if (!steamParticlesInstance.isPlaying)
            {
                steamParticlesInstance.Play();
            }

            float shrinkAmount = shrinkRate * Time.deltaTime;
            transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);

            if (transform.localScale.x <= scaleToDestroyAt)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (steamParticlesInstance != null && steamParticlesInstance.isPlaying)
            {
                steamParticlesInstance.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            isMelting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            isMelting = false;
        }
    }
}
