using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public UIDocument uiDocument;
    private Button CreditsButton;
    [SerializeField] private UIDocument CreditsUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get UI reference
        var root = uiDocument.rootVisualElement;
        CreditsButton = root.Q<Button>("backButton");

        CreditsButton.RegisterCallback<ClickEvent>(CreditsClicked);
    }

    private void CreditsClicked(ClickEvent evt)
    {
        CreditsUI.rootVisualElement.visible = false;
    }
}
