using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMarket : MonoBehaviour
{
    [Tooltip("Food Buttons, will automatcially find children of type UIFoodButton")]
    public UIFoodItem[] foodItems;

    [HideInInspector]
    public int selectedID = -1;

    // food info panel
    public GameObject infoPanel;
    public Image foodImage;
    public Text foodNameText;
    public Text foodInfoText;
    public Transform foodForSaleLayout;
    public Button buyButton;
    public Button nahButton;

    // mapping from buttonID to foodID
    int[] foodIDs;

    List<Food> foods;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        buyButton.onClick.AddListener(OnBuyClicked);
        nahButton.onClick.AddListener(OnNahClicked);

        infoPanel.SetActive(false);
    }

    private void OnEnable()
    {
        foodItems = foodForSaleLayout.GetComponentsInChildren<UIFoodItem>();
        Debug.Log(foodItems.Length);
        foreach (var item in foodItems)
        {
            item.button.onClick.AddListener(() => OnFoodClicked(item.id));
            item.ShowFoodItemInfo();
        }
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
        //gameManager.AddFood()
    }

    public void ShowInfo(int id)
    {
        infoPanel.SetActive(true);

        // assuming buttonID is the order that food is put in the foodForSale list
        foods = gameManager.marketFoods;
        foodNameText.text = foods[id].name;
        foodInfoText.text = "";
        if (foods[id].hp != 0)
            foodInfoText.text += "+ " + foods[id].hp + "MP\n";
        if (foods[id].mp != 0)
            foodInfoText.text += "+ " + foods[id].mp + "MP\n";

        foodInfoText.text += "Price: " + foods[id].price + "$\n\n";

        foodInfoText.text += foods[id].info;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
