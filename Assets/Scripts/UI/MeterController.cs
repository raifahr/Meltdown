using UnityEngine;
using UnityEngine.UIElements;

public class MeterController : MonoBehaviour
{
    public PowerManager PowerManager;

    private void OnEnable()
    {
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        root.Q<SunMeter>().dataSource = PowerManager;
        root.Q<ProgressBar>("acceleration").dataSource = PowerManager;
        root.Q<ProgressBar>("firepower").dataSource = PowerManager;
    }
}
