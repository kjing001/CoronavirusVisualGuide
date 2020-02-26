using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [HideInInspector]
    public int id;

    public Button button;
    public Image image;
    public Text text;
    
    void Awake()
    {
        button = GetComponent<Button>();

        image = GetComponent<Image>();
        if (image == null)
            image = GetComponentInChildren<Image>();

        text = GetComponentInChildren<Text>();

        id = transform.GetSiblingIndex();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        button.onClick.AddListener(OnClicked);
    }

    protected virtual void OnClicked()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
