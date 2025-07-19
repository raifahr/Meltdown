using UnityEngine;
using UnityEngine.UIElements;

public class CountdownTimer : MonoBehaviour
{
    public UIDocument uiDocument;
    public float countdownTime = 60f; // Total time in seconds

    private Label timerLabel;
    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;

        // Get UI reference
        var root = uiDocument.rootVisualElement;
        timerLabel = root.Q<Label>("timer");
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime); // Clamp to 0

            // Format: mm:ss
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            timerLabel.text = $"{minutes:00}:{seconds:00}";
        }
    }
}