using UnityEngine;

public class meltable : MonoBehaviour
{
    public float shrinkRate = 0.8f;
    public float scaleToDestroyAt = 0.2f;

    private bool isMelting = false;

    void Update()
    {
        if (isMelting)
        {
            float shrinkAmount = shrinkRate * Time.deltaTime;
            transform.localScale -= new Vector3(shrinkAmount, shrinkAmount, shrinkAmount);

            if (transform.localScale.x <= scaleToDestroyAt)
            {
                Destroy(gameObject);
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
