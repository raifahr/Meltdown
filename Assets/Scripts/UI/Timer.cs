using UnityEngine;
using UnityEngine.UIElements;

public class CountdownTimer : MonoBehaviour
{
    public UIDocument uiDocument;
    public float countdownTime = 60f; // Total time in seconds
    [SerializeField] private GameObject audioListener;
    [SerializeField] private UIDocument PauseUI;
    [SerializeField] private UIDocument GameOverUI;

    private Label timerLabel;
    private Label ScoreLabel;
    private Button PauseButton;
    private Button SoundButton;
    private float currentTime;
    public int Score = 0;
    public float timeRemaining;

    public bool SoundToggle = true;
    public bool PauseToggle = true;


    public static CountdownTimer Instance;

    void Awake()
    {
        Instance = this;
    }


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
        GameOverUI.rootVisualElement.visible = false;
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

        if (currentTime <= 0.0f)
        {
            GameOverUI.rootVisualElement.visible = true;
            Time.timeScale = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameOverUI.rootVisualElement.visible == false)
        {
            PauseToggle = !PauseToggle;
        }

        if (PauseToggle)
        {
            Time.timeScale = 0.0f;
            PauseUI.rootVisualElement.visible = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            PauseUI.rootVisualElement.visible = false;
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

        ScoreLabel.text = "Score: " + Score.ToString();

       timeRemaining = currentTime;
    }
}