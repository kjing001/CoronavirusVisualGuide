using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorld : MonoBehaviour
{
    public Button marketButton;
    public Button homeButton;
    public Button marketExitButton;
    public Button homeExitButton;
    public GameObject marketPanel;
    public GameObject homePanel;
    public GameObject background;


    // Start is called before the first frame update
    void Awake()
    {
        marketButton.onClick.AddListener(()=>ShowMarket(true));
        homeButton.onClick.AddListener(()=>ShowHome(true));
        marketExitButton.onClick.AddListener(() => ShowMarket(false));
        homeExitButton.onClick.AddListener(() => ShowHome(false));
        ShowMarket(false);
    }

    void ShowMarket(bool show)
    {
        marketPanel.SetActive(show);
        homeButton.gameObject.SetActive(!show);
        marketButton.gameObject.SetActive(!show);
        background.SetActive(!show);
    }
    
    void ShowHome(bool show)
    {
        homePanel.SetActive(show);
        homeButton.gameObject.SetActive(!show);
        marketButton.gameObject.SetActive(!show);
        background.SetActive(!show);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
