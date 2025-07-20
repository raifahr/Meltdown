using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerManager : MonoBehaviour
{
    public float CurrPower = 0.0f;
    public float CurrAcceleration = 0.0f;
    public float CurrFirepower = 0.0f;
    public int Health = 5;

    private float MaxPower = 100.0f;
    private float GrowRate; // regen
    private float ConsumeRate; // drain

    public PlayerStamina stamina;
    [SerializeField] private UIDocument GameOverUI;

    private Dictionary<int, bool> mouseButtonMap = new Dictionary<int, bool>()
    {
        { 0, false }, // Left click = Boost
        { 1, false }  // Right click = Fire
    };

    public bool Boosting => mouseButtonMap[0];
    public bool Firing => mouseButtonMap[1];

    void Start()
    {
        if (stamina != null)
        {
            GrowRate = stamina.staminaRegenRate;
            ConsumeRate = stamina.staminaDrainRate;
            CurrPower = MaxPower;
        }
        else
        {
            Debug.LogWarning("PowerManager: PlayerStamina reference not set!");
        }
    }

    void Update()
    {
        if (Health <= 0)
        {
            GameOverUI.rootVisualElement.visible = true;
            Time.timeScale = 0.0f;
        }
        for (int button = 0; button < 2; button++)
        {
            if (Input.GetMouseButtonDown(button))
            {
                mouseButtonMap[button] = true;
                mouseButtonMap[1 - button] = false;
            }

            if (Input.GetMouseButtonUp(button))
            {
                mouseButtonMap[button] = false;
            }
        }

        if (CurrPower <= 0.0f)
        {
            mouseButtonMap[0] = false;
            mouseButtonMap[1] = false;
        }

        float delta = Time.deltaTime;
        float energyUsed = ConsumeRate * delta;
        float energyRegen = GrowRate * delta;

        float EnergyConsumptionVal = ConsumeRate * Time.deltaTime;

        if (Boosting)
        {
            CurrPower -= EnergyConsumptionVal;
            CurrAcceleration += EnergyConsumptionVal;
            CurrFirepower -= EnergyConsumptionVal;
        }
        else if (Firing)
        {
            CurrPower -= ConsumeRate * Time.deltaTime;
            CurrFirepower += EnergyConsumptionVal;
            CurrAcceleration -= EnergyConsumptionVal;
        }
        else
        {
            CurrPower += GrowRate * Time.deltaTime;
            CurrAcceleration -= EnergyConsumptionVal/2;
            CurrFirepower -= EnergyConsumptionVal/2;
        }

        CurrPower = Mathf.Clamp(CurrPower, 0.0f, MaxPower);
        CurrAcceleration = Mathf.Clamp(CurrAcceleration, 0.0f, MaxPower);
        CurrFirepower = Mathf.Clamp(CurrFirepower, 0.0f, MaxPower);
    }

    public void ModifyHealth(int val)
    {
        Health += val;
    }
}
