using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorld : MonoBehaviour
{
    public Button marketButton;
    public Button marketExitButton;
    public GameObject market;
    public GameObject background;

    // Start is called before the first frame update
    void Awake()
    {
        marketButton.onClick.AddListener(()=>ShowMarket(true));
        marketExitButton.onClick.AddListener(() => ShowMarket(false));
        ShowMarket(false);
    }

    void ShowMarket(bool show)
    {
        market.SetActive(show);
        marketButton.gameObject.SetActive(!show);
        background.SetActive(!show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
