using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHomeFood : MonoBehaviour
{
    public Text foodInfoText;
    public GameObject panel;
    public UIInventorySlot slotPrefab;
    public Transform content;

    public Text consoleText;

    public Button eatButton;
    GameManager manager;

    public List<UIFoodItem> myFoodItems;

    public Button closeButton;

    int selectedID;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        eatButton.onClick.AddListener(OnEat);
    }

    private void OnEnable()
    {
        var items = GetComponentsInChildren<UIFoodItem>();
        for (int i = 0; i < items.Length; i++)
        {
            myFoodItems.Add(items[i]);
            items[i].button.onClick.AddListener(() => OnFoodClicked(items[i].id));
        }
    }
    private void OnDisable()
    {
        var items = GetComponentsInChildren<UIFoodItem>();
        foreach (var item in items)
        {
            item.button.onClick.RemoveAllListeners();
        }
    }

    void OnFoodClicked(int id)
    {
        selectedID = id;
    }

    private void OnEat()
    {
        if (selectedID == -1)
            return;

        print("selected ID: " + selectedID);

        Food selectedFood = manager.myFoods[selectedID];
        manager.hp += selectedFood.hp;
        manager.mp += selectedFood.mp;
        consoleText.text = "Used " + selectedFood.name +
            ", + " + selectedFood.hp + "HP , + " + selectedFood.mp + "MP";

        manager.myFoods.Remove(selectedFood);
        myFoodItems.RemoveAt(selectedID);
        Destroy(myFoodItems[selectedID].gameObject);
        //myFoodItems = GetComponentsInChildren<UIFoodItem>();
        foreach (var item in myFoodItems)
        {
            item.button.onClick.RemoveAllListeners();
            item.button.onClick.AddListener(() => OnFoodClicked(item.id));
        }
        selectedID = -1;
    }

    public void UpdatePanel()
    {
        if (panel.activeSelf)
        {
            // instantiate/destroy enough slots
            UIUtils.BalancePrefabs(slotPrefab.gameObject, manager.myFoods.Count, content);

            // refresh all items
            for (int i = 0; i < manager.myFoods.Count; ++i)
            {
                UIInventorySlot slot = content.GetChild(i).GetComponent<UIInventorySlot>();
                
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
