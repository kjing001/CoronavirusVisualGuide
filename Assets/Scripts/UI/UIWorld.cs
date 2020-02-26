using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorld : MonoBehaviour
{
    public Button marketButton;
    public Button foodButton;
    public Button goOutButton;

    // Start is called before the first frame update
    void Start()
    {
        marketButton.onClick.AddListener(OnMarketClicked);
    }

    private void OnMarketClicked()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
