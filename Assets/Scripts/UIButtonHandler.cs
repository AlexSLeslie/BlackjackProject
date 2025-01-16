using UnityEngine;
using UnityEngine.UIElements;

public class UIButtonHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        
        var root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<Button>("HitButton").clicked += () => OnButtonClick(root.Q<Button>("HitButton"));
        root.Q<Button>("StandButton").clicked += () => OnButtonClick(root.Q<Button>("StandButton"));

    }

    void OnButtonClick(Button button){
        Debug.Log($"- {button.name}");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
