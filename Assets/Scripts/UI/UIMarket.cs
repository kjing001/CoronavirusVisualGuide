using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMarket : MonoBehaviour
{
    [Tooltip("Food Buttons, will automatcially find children of type UIFoodButton")]
    public Button[] foodButtons;

    [HideInInspector]
    public int selectedID = -1;

    // food info panel
    public GameObject infoPanel;
    public Image foodImage;
    public Text foodNameText;
    public Text foodInfoText;

    public Button buyButton;
    public Button nahButton;

    // mapping from buttonID to foodID
    int[] foodIDs;

    List<Food> foodsForSale;

    // Start is called before the first frame update
    void Start()
    {
        foodButtons = GetComponentsInChildren<Button>();
        for (int i = 0; i < foodButtons.Length; i++)
        {
            int temp = i;
            foodButtons[i].onClick.AddListener(() => OnFoodClicked(temp));
        }

        buyButton.onClick.AddListener(OnBuyClicked);
        nahButton.onClick.AddListener(OnNahClicked);

        infoPanel.SetActive(false);
        foodsForSale = FoodManager.instance.foodsForSale;
    }

    void OnFoodClicked(int id)
    {
        selectedID = id;
        ShowInfo(selectedID);
    }

    private void OnNahClicked()
    {
        infoPanel.SetActive(false);
        selectedID = -1;
    }

    private void OnBuyClicked()
    {
        
    }

    public void ShowInfo(int id)
    {
        infoPanel.SetActive(true);

        // assuming buttonID is the order that food is put in the foodForSale list

        foodNameText.text = foodsForSale[id].name;
        foodInfoText.text = "";
        if (foodsForSale[id].hp != 0)
            foodInfoText.text += "+ " + foodsForSale[id].hp + "MP\n";
        if (foodsForSale[id].mp != 0)
            foodInfoText.text += "+ " + foodsForSale[id].mp + "MP\n";

        foodInfoText.text += "Price: " + foodsForSale[id].price + "$\n\n";

        foodInfoText.text += foodsForSale[id].info;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
