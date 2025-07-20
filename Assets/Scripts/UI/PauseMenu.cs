using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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

    }

    private void ResumeClicked(ClickEvent evt)
    {
        timer.PauseToggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
