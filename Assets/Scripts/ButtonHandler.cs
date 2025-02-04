using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class ButtonHandler : MonoBehaviour
{
    public enum Root{
        HIT_STAND,
        TEST
    }
    public UIDocument HitStandDoc;
    public UIDocument TestDoc;
    public GameLogic gameLogic;

    Dictionary<Root, VisualElement> roots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        roots = new Dictionary<Root, VisualElement>();

        var HitStandRoot =  HitStandDoc.rootVisualElement;
        HitStandRoot.Q<Button>("HitButton").clicked += () => OnButtonClick(HitStandRoot.Q<Button>("HitButton"));
        HitStandRoot.Q<Button>("StandButton").clicked += () => OnButtonClick(HitStandRoot.Q<Button>("StandButton"));
        roots.Add(Root.HIT_STAND, HitStandRoot);
        
        var TestRoot = TestDoc.rootVisualElement;
        TestRoot.Q<Button>("TestButton").clicked += () => OnButtonClick(TestRoot.Q<Button>("TestButton"));
        roots.Add(Root.TEST, TestRoot);

        foreach(VisualElement visualElement in roots.Values.ToList())
            HideElement(visualElement);

    }

    void OnButtonClick(Button button){
        Debug.Log($"{button.name}");

        if(button.name == "HitButton")
            gameLogic.Hit();
        
        
    }

    public void HideElement(VisualElement visualElement){ visualElement.style.visibility = Visibility.Hidden; }

    public void HideElement(Root r){ HideElement(roots[r]); }

    public void ShowElement(VisualElement visualElement){ visualElement.style.visibility = Visibility.Visible; }

    public void ShowElement(Root r){ ShowElement(roots[r]); }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
