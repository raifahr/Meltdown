using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public UIDocument uiDocument;
    public CountdownTimer timer;
    private Button ResumeButton;
    private Button ControlsButton;
    private Button QuitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get UI reference
        var root = uiDocument.rootVisualElement;
        ResumeButton = root.Q<Button>("resume");
        ControlsButton = root.Q<Button>("controls");
        QuitButton = root.Q<Button>("quit");

        ResumeButton.RegisterCallback<ClickEvent>(ResumeClicked);
        QuitButton.RegisterCallback<ClickEvent>(QuitClicked);
    }

    private void ResumeClicked(ClickEvent evt)
    {
        timer.PauseToggle = false;
    }

    private void QuitClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
