using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMarket : MonoBehaviour
{
    [Tooltip("Food Buttons, will automatcially find children of type UIFoodButton")]
    public UIFoodButton[] foodButtons;

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

    // Start is called before the first frame update
    void Start()
    {
        foodButtons = GetComponentsInChildren<UIFoodButton>();

        buyButton.onClick.AddListener(OnBuyClicked);
        nahButton.onClick.AddListener(OnNahClicked);

        infoPanel.SetActive(false);
    }

    void UpdateFoodSale()
    {
        foodIDs = new int[] { 0, 1, 2, 3, 4, 5 };
    }

    private void OnNahClicked()
    {
        infoPanel.SetActive(false);
        selectedID = -1;
    }

    private void OnBuyClicked()
    {
        
    }

    public void ShowInfo(int buttonID)
    {
        infoPanel.SetActive(true);

        // to-do load food info by food id


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
