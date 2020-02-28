using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFoodItem : UIItem
{
    // get the food in myFoods list
    public Food food;
    
    public void ShowFoodItemInfo()
    {
        text.text = food.name;

        // To-do: show image, function, etc.
    }

}
