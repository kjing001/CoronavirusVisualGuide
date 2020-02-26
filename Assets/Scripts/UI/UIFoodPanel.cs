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

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
        eatButton.onClick.AddListener(OnEat);
    }

    private void OnEat()
    {
        if (manager.food > 0)        
            manager.food--;
        else
        {
            // no food to eat
            promptText.text = "Need More Food!";
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
