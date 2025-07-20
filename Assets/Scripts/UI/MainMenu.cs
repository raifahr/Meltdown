using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public UIDocument uiDocument;
    private Button StartButton;
    private Button CreditsButton;
    [SerializeField] private UIDocument CreditsUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get UI reference
        var root = uiDocument.rootVisualElement;
        StartButton = root.Q<Button>("start");
        CreditsButton = root.Q<Button>("credits");

        StartButton.RegisterCallback<ClickEvent>(StartClicked);
        CreditsButton.RegisterCallback<ClickEvent>(CreditsClicked);
        CreditsUI.rootVisualElement.visible = false;

    }

    private void CreditsClicked(ClickEvent evt)
    {
        CreditsUI.rootVisualElement.visible = true;
    }

    private void StartClicked(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
