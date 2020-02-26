using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFoodBar : UIBar
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        manager.OnFoodChange.AddListener(OnValueChange);
        maxValue = manager.maxFood;
        OnValueChange();

        print(maxLength);
        print(maxValue);
    }

    protected override void OnValueChange()
    {
        base.OnValueChange();

        print(maxLength);
        print(maxValue);

        // update the visuals of food bar
        value = manager.food;
        currentLenght = value / maxValue * maxLength;
        front.sizeDelta = new Vector2(currentLenght, height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
