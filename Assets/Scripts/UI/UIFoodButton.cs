using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFoodButton : UIButton
{    

    UIMarket market;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        market = FindObjectOfType<UIMarket>();
    }

    protected override void OnClicked()
    {
        market.selectedID = id;

        market.ShowInfo(market.selectedID);

        //debug info
        print("selectedID: " + market.selectedID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
