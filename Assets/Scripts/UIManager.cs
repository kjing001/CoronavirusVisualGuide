using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    bool button1Clicked;
    bool button2Clicked;
    bool button3Clicked;

    public Button nextButton;

    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(OnButton1Clicked);
        button2.onClick.AddListener(OnButton2Clicked);
        button3.onClick.AddListener(OnButton3Clicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
        nextButton.gameObject.SetActive(false);
    }

    private void OnNextButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    private void OnButton3Clicked()
    {
        button3Clicked = true;
        nextButton.gameObject.SetActive(button1Clicked && button2Clicked && button3Clicked);
    }

    private void OnButton2Clicked()
    {
        button2Clicked = true;
        nextButton.gameObject.SetActive(button1Clicked && button2Clicked && button3Clicked);
    }

    private void OnButton1Clicked()
    {
        button1Clicked = true;
        nextButton.gameObject.SetActive(button1Clicked && button2Clicked && button3Clicked);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
