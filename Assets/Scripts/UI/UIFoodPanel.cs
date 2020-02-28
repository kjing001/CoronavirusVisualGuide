using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodPanel : MonoBehaviour
{
    public Text foodCountText;
    public Text foodInfoText;

    public Text promptText;

    public Button eatButton;
    GameManager manager;

    public Button[] foodButtons;

    public Transform foodLayout;

    int selectedID;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        eatButton.onClick.AddListener(OnEat);

        foodButtons = GetComponentsInChildren<Button>();
        for (int i = 0; i < foodButtons.Length; i++)
        {
            int t = i;
            foodButtons[i].onClick.AddListener(() => OnFoodClicked(t));
        }

    }

    void OnFoodClicked(int id)
    {
        selectedID = id;
    }

    private void OnEat()
    {
        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
