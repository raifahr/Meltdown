using UnityEngine;

public class meltable : MonoBehaviour
{
    public float shrinkRate = 0.8f;
    public float scaleToDestroyAt = 0.2f;
    public ParticleSystem steamParticlePrefab;

    private ParticleSystem steamParticlesInstance;
    private bool isMelting = false;
    private bool hasMelted = false;

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

            if (!hasMelted && transform.localScale.x <= scaleToDestroyAt)
            {
                hasMelted = true;

                if (CountdownTimer.Instance != null && CountdownTimer.Instance.timeRemaining > 0f)
                {
                    CountdownTimer.Instance.Score += 1;
                }

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
