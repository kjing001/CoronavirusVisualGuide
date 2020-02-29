using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHomeFoodPanel : MonoBehaviour
{
    public Text foodCountText;
    public Text foodInfoText;

    public Text consoleText;

    public Button eatButton;
    GameManager manager;

    public UIFoodItem[] myFoodItems;

    public Button closeButton;

    int selectedID;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        eatButton.onClick.AddListener(OnEat);
        closeButton.onClick.AddListener(OnClose);
    }

    private void OnEnable()
    {
        myFoodItems = GetComponentsInChildren<UIFoodItem>();
        foreach (var item in myFoodItems)
        {
            item.button.onClick.AddListener(() => OnFoodClicked(item.id));
        }
    }

    private void OnClose()
    {
        gameObject.SetActive(false);
    }

    void OnFoodClicked(int id)
    {
        selectedID = id;
    }

    private void OnEat()
    {
        Food selectedFood = myFoodItems[selectedID].food;
        manager.hp += selectedFood.hp;
        manager.mp += selectedFood.mp;
        consoleText.text = "Used " + selectedFood.name +
            ", + " + selectedFood.hp + "HP , + " + selectedFood.mp + "MP";

        manager.myFoods.Remove(selectedFood);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
