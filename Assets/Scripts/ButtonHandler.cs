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
        Debug.Log($"- {button.name}");
    }

    public void HideElement(VisualElement visualElement){
        visualElement.style.visibility = Visibility.Hidden;
    }

    public void ShowElement(VisualElement visualElement){
        visualElement.style.visibility = Visibility.Visible;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
