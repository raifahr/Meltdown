using UnityEngine;

public class burnable : MonoBehaviour
{
    public float darkenRate = 0.5f;
    public float destroyThreshold = 0.5f;
    public ParticleSystem burnEffectPrefab;

    private ParticleSystem burnEffectInstance;
    private bool isBurning = false;
    private Material materialInstance;
    private Color originalColor;
    private float darkenAmount = 0f;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            materialInstance = renderer.material;
            originalColor = materialInstance.color;
        }
    }

    void Update()
    {
        if (isBurning && materialInstance != null)
        {
            if (burnEffectInstance == null)
            {
                burnEffectInstance = Instantiate(burnEffectPrefab, transform.position, Quaternion.identity, transform);
                burnEffectInstance.Play();
            }

            darkenAmount += darkenRate * Time.deltaTime;
            darkenAmount = Mathf.Clamp01(darkenAmount);

            Color newColor = Color.Lerp(originalColor, Color.black, darkenAmount);
            materialInstance.color = newColor;

            if (darkenAmount >= destroyThreshold)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (burnEffectInstance != null && burnEffectInstance.isPlaying)
            {
                burnEffectInstance.Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            isBurning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flame"))
        {
            isBurning = false;
        }
    }
}
