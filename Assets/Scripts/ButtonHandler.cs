using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

// TODO: Clean up OnEnable() with querybuilder code

public class ButtonHandler : MonoBehaviour
{
    public UIDocument HitStandDoc;
    public UIDocument TestDoc;
    public UIDocument TotalDoc;

    public GameLogic gameLogic;

    Dictionary<string, VisualElement> roots;
    Dictionary<string, Label> labels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        // Root visual elements added to dictionary for easier access later, ie when hiding all elements
        roots = new Dictionary<string, VisualElement>();
        labels = new Dictionary<string, Label>();

        var HitStandRoot =  HitStandDoc.rootVisualElement;
        HitStandRoot.Q<Button>("HitButton").clicked += () => OnButtonClick(HitStandRoot.Q<Button>("HitButton"));
        HitStandRoot.Q<Button>("StandButton").clicked += () => OnButtonClick(HitStandRoot.Q<Button>("StandButton"));
        roots.Add("HitStand", HitStandRoot);
        
        var TestRoot = TestDoc.rootVisualElement;
        TestRoot.Q<Button>("TestButton").clicked += () => OnButtonClick(TestRoot.Q<Button>("TestButton"));
        roots.Add("Test", TestRoot);

        var TotalRoot = TotalDoc.rootVisualElement;
        roots.Add("Total", TotalRoot);
        labels.Add("DealerTotalLabel", TotalRoot.Q<Label>("DealerTotalLabel"));
        labels.Add("PlayerTotalLabel", TotalRoot.Q<Label>("PlayerTotalLabel"));
        

        foreach(VisualElement visualElement in roots.Values.ToList())
            HideElement(visualElement);

    }

    void OnButtonClick(Button button){
        Debug.Log($"{button.name}");

        if(button.name == "HitButton")
            gameLogic.Hit();

        if(button.name == "StandButton")
            gameLogic.Stand();
        
        
    }

    public void SetLabelText(string label, string text){ labels[label].text = text; }

    public void HideElement(VisualElement visualElement){ visualElement.style.visibility = Visibility.Hidden; }

    public void HideElement(string r){ HideElement(roots[r]); }

    public void ShowElement(VisualElement visualElement){ visualElement.style.visibility = Visibility.Visible; }

    public void ShowElement(string r){ ShowElement(roots[r]); }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
