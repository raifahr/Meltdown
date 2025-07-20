using UnityEngine;
using UnityEngine.UIElements;

public class CountdownTimer : MonoBehaviour
{
    public UIDocument uiDocument;
    public float countdownTime = 60f; // Total time in seconds
    [SerializeField] private GameObject audioListener;
    [SerializeField] private GameObject PauseUI;

    private Label timerLabel;
    private Label ScoreLabel;
    private Button PauseButton;
    private Button SoundButton;
    private float currentTime;

    public bool SoundToggle = true;
    public bool PauseToggle = false;

    void Start()
    {
        currentTime = countdownTime;

        // Get UI reference
        var root = uiDocument.rootVisualElement;
        timerLabel = root.Q<Label>("timer");
        ScoreLabel = root.Q<Label>("Score");
        PauseButton = root.Q<Button>("Pause");
        PauseButton.RegisterCallback<ClickEvent>(PauseClicked);
        SoundButton = root.Q<Button>("Sound");
        SoundButton.RegisterCallback<ClickEvent>(SoundClicked);
    }

    private void PauseClicked(ClickEvent evt)
    {
        PauseToggle = !PauseToggle;
    }

    private void SoundClicked(ClickEvent evt)
    {
        SoundToggle = !SoundToggle;
    }

    void Update()
    {
        audioListener.GetComponent<AudioListener>().enabled = SoundToggle;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseToggle = !PauseToggle;
        }

        if (PauseToggle)
        {
            Time.timeScale = 0.0f;
            PauseUI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            PauseUI.SetActive(false);
        }

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