using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFood : MonoBehaviour
{
    public Text foodCountText;
    public Text foodInfoText;

    public Button eatButton;

    

    // Start is called before the first frame update
    void Start()
    {
        eatButton.onClick.AddListener(OnEat);
    }

    private void OnEat()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
