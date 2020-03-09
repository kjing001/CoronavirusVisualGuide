using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodMarket : MonoBehaviour
{
    public int selectedID = -1;
    int m_Amount;
    int amount { get { return m_Amount; } set { m_Amount = value; amountText.text = value.ToString(); } }

    float unitPrice;
    float m_Price;
    float price { get { return m_Price; } set { m_Price = value; priceText.text = value.ToString(); } }

    public UIMarketSlot slotPrefab;
    // food info panel
    public GameObject infoPanel;
    public Image foodImage;
    public Text foodInfoText;
    public Text amountText;
    public Text priceText;
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
        plusButton.onClick.AddListener(OnPlusClicked);
        minusButton.onClick.AddListener(OnMinusClicked);

        infoPanel.SetActive(false);
        selectedID = -1;
        amount = 0;
    }

    private void OnMinusClicked()
    {
        amount = (amount > 1) ? amount - 1 : 1;
        price = unitPrice * amount;

        buyButton.interactable = amount > 0 && price <= player.money;
    }

    private void OnPlusClicked()
    {
        if (selectedID == -1)
        {
            amount++;
        }
        else
        {
            int maxAmount = gameManager.saleFood[selectedID].amount;
            amount = (amount < maxAmount) ? amount + 1 : maxAmount;
            price = unitPrice * amount;
        }

        buyButton.interactable = amount > 0 && price <= player.money;
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
            amount = 1;
            unitPrice = gameManager.saleFood[selectedID].item.price;
            price = unitPrice;
            ShowFoodTooltip(selectedID);
        }
        foodImage.sprite = gameManager.saleFood[selectedID].item.image;
    }

    private void OnBuyClicked()
    {
        if (selectedID == -1 || amount <= 0)
            return;

        player.BuyFood(selectedID, amount);
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
