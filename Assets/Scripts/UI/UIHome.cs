using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHome : MonoBehaviour
{
    public GameObject foodPanel;

    public Button newsButton;
    public Button foodButton;
    public Button goOutButton;

    public 

    // Start is called before the first frame update
    void Start()
    {
        foodPanel.SetActive(false);
        foodButton.onClick.AddListener(OnFood);
    }

    private void OnFood()
    {
        foodPanel.SetActive(!foodPanel.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
