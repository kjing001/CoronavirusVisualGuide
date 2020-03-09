using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHomeFood : MonoBehaviour
{
    public Text foodToolTipText;
    public GameObject panel;
    public UIInventorySlot slotPrefab;
    public Transform content;

    public Text consoleText;

    public Button eatButton;
    GameManager manager;
    Player player;

    public Button closeButton;

    int selectedID;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        player = Player.instance;
        panel.SetActive(false);
        eatButton.onClick.AddListener(OnEatClicked);
    }

    private void OnEnable()
    {
        if (player == null)
            player = Player.instance;

        UpdatePanel();
        ShowUnselectedState();
    }
    
    void ShowUnselectedState()
    {
        selectedID = -1;

        // hide tooltip in the tooltip area
        foodToolTipText.text = "";

        // hide the eat button
        eatButton.gameObject.SetActive(false);
    }

    void OnFoodSlotClicked(int id)
    {
        if (selectedID != id)
        {
            selectedID = id;

            // show tooltip in the tooltip area
            foodToolTipText.text = player.food[selectedID].item.ToolTip();

            // show the eat button
            eatButton.gameObject.SetActive(true);
        }
        else
        {
            ShowUnselectedState();
        }
    }

    void OnEatClicked()
    {
        if (selectedID == -1)
            return;

        print("Eating selected ID: " + selectedID);

        player.EatFood(selectedID);

        // reset listeners and items
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        if (panel.activeSelf)
        {            
            // instantiate/destroy enough slots
            UIUtils.BalancePrefabs(slotPrefab.gameObject, player.food.Count, content);

            // refresh all items
            for (int i = 0; i < player.food.Count; ++i)
            {
                UIInventorySlot slot = content.GetChild(i).GetComponent<UIInventorySlot>();
                FoodItemAndAmount food = player.food[i];

                if (food.amount > 0)
                {
                    int icopy = i; // needed for lambdas, otherwise i is Count
                    slot.button.onClick.SetListener(() => OnFoodSlotClicked(icopy));

                    // show images and amount text
                    slot.image.color = Color.white;
                    slot.image.sprite = food.item.image;
                    slot.amountText.text = food.amount.ToString();

                }
                else
                {
                    // refresh invalid item
                    slot.button.onClick.RemoveAllListeners();
                    slot.image.color = Color.clear;
                    slot.image.sprite = null;
                    slot.cooldownCircle.fillAmount = 0;
                    slot.amountOverlay.SetActive(false);
                }

            }
        }
        
              
    }

    // Update is called once per frame
    void Update()
    {

    }
}
