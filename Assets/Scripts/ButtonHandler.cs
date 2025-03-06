using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

// TODO: Clean up OnEnable() with querybuilder code

public class ButtonHandler : MonoBehaviour
{
    public UIDocument HitStandDoc;
    public UIDocument RestartDoc;
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
        
        var RestartRoot = RestartDoc.rootVisualElement;
        RestartRoot.Q<Button>("RestartButton").clicked += () => OnButtonClick(RestartRoot.Q<Button>("RestartButton"));
        roots.Add("Restart", RestartRoot);

        var TotalRoot = TotalDoc.rootVisualElement;
        roots.Add("Total", TotalRoot);


        // Theres probably a more elegant way to do this but ;-;
        labels.Add("DealerTotalLabel", TotalRoot.Q<Label>("DealerTotalLabel"));
        labels.Add("PlayerTotalLabel", TotalRoot.Q<Label>("PlayerTotalLabel"));
        labels.Add("ChipsLabel", TotalRoot.Q<Label>("ChipsLabel"));
        
        HideAll();
        

    }

    void OnButtonClick(Button button){
        Debug.Log(button.name + " clicked");
        
        if(button.name == "HitButton")
            gameLogic.Hit();

        if(button.name == "StandButton")
            gameLogic.Stand();

        if(button.name == "RestartButton")
            if(GetBet() <= gameLogic.chips){
                button.text = "Play";
                gameLogic.Restart();
            }
            else button.text = "Not enough chips!";
        
        
    }


    public void EnableBet(bool value){ TotalDoc.rootVisualElement.Q<IntegerField>("BetField").SetEnabled(value); } 

    // Hide all visual elements
    public void HideAll(){ 
        foreach(VisualElement visualElement in roots.Values.ToList())
            HideElement(visualElement);
    }

    public int GetBet(){ return TotalDoc.rootVisualElement.Q<IntegerField>("BetField").value; }

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
