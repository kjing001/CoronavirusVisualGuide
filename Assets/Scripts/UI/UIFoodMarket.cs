using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodMarket : MonoBehaviour
{
    public int selectedID = -1;
    int amount;
    public UIMarketSlot slotPrefab;
    // food info panel
    public GameObject infoPanel;
    public Image foodImage;
    public Text foodInfoText;
    public Text amountText;
    public Transform content;
    public Button buyButton;
    public Button plusButton;
    public Button minusButton;  

    GameManager gameManager;
    Player player;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        player = Player.instance;

        buyButton.onClick.AddListener(OnBuyClicked);

        infoPanel.SetActive(false);
    }

    private void OnEnable()
    {
        UpdatePanel();
    }
    private void OnDisable()
    {
        selectedID = -1;
    }

    void UpdatePanel()
    {
        if (gameManager == null)
            gameManager = GameManager.instance;

        int length = gameManager.saleFood.Count;
        UIUtils.BalancePrefabs(slotPrefab.gameObject, length, content);

        for (int i = 0; i < length; i++)
        {
            UIMarketSlot slot = content.GetChild(i).GetComponent<UIMarketSlot>();

            FoodItemAndAmount foodItemAndAmount = gameManager.saleFood[i];

            // add select id
            int icopy = i;
            slot.button.onClick.SetListener(() => OnFoodClicked(icopy));

            // show item in UI
            slot.image.color = Color.white;
            slot.image.sprite = foodItemAndAmount.item.image;
            slot.nameText.text = "";
            slot.amountText.text = foodItemAndAmount.amount.ToString();
        }
    }

    void OnFoodClicked(int id)
    {
        if (selectedID != id)
        {
            selectedID = id;
            amount = 0;
            ShowFoodTooltip(selectedID);

        }
        
    }

    private void OnBuyClicked()
    {
        if (selectedID == -1)
            return;

        player.BuyFood(selectedID);
    }

    public void ShowFoodTooltip(int id)
    {
        infoPanel.SetActive(true);

        FoodItem food = gameManager.saleFood[id].item;
        foodInfoText.text = food.ToolTip();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
