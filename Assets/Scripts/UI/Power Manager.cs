using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerManager : MonoBehaviour
{
    public float CurrPower = 0.0f;
    private float MaxPower = 100.0f;
    private float GrowRate = 10.0f;
    private float ConsumeRate = 25.0f;

    private Dictionary<int, bool> mouseButtonMap = new Dictionary<int, bool>()
    {
        { 0, false }, // Left click = Boost
        { 1, false }  // Right click = Fire
    };

    public bool Boosting => mouseButtonMap[0];
    public bool Firing => mouseButtonMap[1];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        if (!Boosting && !Firing)
        {
            CurrPower += GrowRate * Time.deltaTime;
        }
        else
        {
            CurrPower -= ConsumeRate * Time.deltaTime;
        }

        CurrPower = Mathf.Clamp(CurrPower, 0.0f, MaxPower);
    }
}
