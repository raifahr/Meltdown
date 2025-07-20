using UnityEngine;
using UnityEngine.UIElements;

public class MeterController : MonoBehaviour
{
    public PowerManager PowerManager;

    private void OnEnable()
    {
        VisualElement root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        root.Q<SunMeter>().dataSource = PowerManager;
        var aBar = root.Q<ProgressBar>("acceleration");
        var inneraBar = aBar.Q(className: "unity-progress-bar__progress");
        aBar.dataSource = PowerManager;
        inneraBar.style.backgroundColor = Color.blue;

        var fBar = root.Q<ProgressBar>("firepower");
        var innerfBar = fBar.Q(className: "unity-progress-bar__progress");
        fBar.dataSource = PowerManager;
        innerfBar.style.backgroundColor = Color.red;

        var hBar = root.Q<ProgressBar>("healthBar");
        var innerhBar = hBar.Q(className: "unity-progress-bar__progress");
        innerhBar.style.backgroundColor = Color.green;
        hBar.dataSource = PowerManager;
    }
}
