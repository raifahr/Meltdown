using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 3.0f;
    public float staminaDrainRate = 1.0f;
    public float staminaRegenRate = 0.5f;

    private float currentStamina;

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    void Start()
    {
        currentStamina = maxStamina;
    }

    public bool HasStamina()
    {
        return currentStamina > 0.0f;
    }

    public void DrainStamina()
    {
        currentStamina -= staminaDrainRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0.0f, maxStamina);
    }

    public void RegenerateStamina()
    {
        currentStamina += staminaRegenRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0.0f, maxStamina);
    }
}


