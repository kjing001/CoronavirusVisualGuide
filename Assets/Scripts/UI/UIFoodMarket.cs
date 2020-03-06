using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodMarket : MonoBehaviour
{
    public int selectedID = -1;
    public UIMarketSlot slotPrefab;
    // food info panel
    public GameObject infoPanel;
    public Image foodImage;
    public Text foodNameText;
    public Text foodInfoText;
    public Transform content;
    public Button buyButton;
    public Button nahButton;
    
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
        nahButton.onClick.AddListener(OnNahClicked);

        infoPanel.SetActive(false);
    }

    private void OnEnable()
    {
        UpdatePanel();
    }

    void UpdatePanel()
    {
        if (gameManager == null)
            gameManager = GameManager.instance;

        int length = gameManager.saleFoodItems.Count;
        UIUtils.BalancePrefabs(slotPrefab.gameObject, length, content);

        for (int i = 0; i < length; i++)
        {
            UIMarketSlot slot = content.GetChild(i).GetComponent<UIMarketSlot>();
            FoodItem foodItemData = gameManager.saleFoodItems[i];


            // add select id
            int icopy = i;
            slot.button.onClick.SetListener(() => OnFoodClicked(icopy));

            // show item in UI
            slot.image.color = Color.white;
            slot.image.sprite = foodItemData.image;
        }
    }

    void OnFoodClicked(int id)
    {
        selectedID = id;
        ShowFoodTooltip(selectedID);
    }

    private void OnNahClicked()
    {
        infoPanel.SetActive(false);
        selectedID = -1;
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

        FoodItem food = gameManager.saleFoodItems[id];
        foodNameText.text = food.name;
        foodInfoText.text = food.ToolTip();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
