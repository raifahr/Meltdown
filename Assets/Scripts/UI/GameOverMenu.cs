using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public UIDocument uiDocument;
    private Button RestartButton;
    private Button QuitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get UI reference
        var root = uiDocument.rootVisualElement;
        RestartButton = root.Q<Button>("resume");
        QuitButton = root.Q<Button>("quit");

        RestartButton.RegisterCallback<ClickEvent>(ResumeClicked);
        QuitButton.RegisterCallback<ClickEvent>(QuitClicked);
    }

    private void ResumeClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
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
