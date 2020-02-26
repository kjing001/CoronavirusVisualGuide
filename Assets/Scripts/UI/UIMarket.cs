using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMarket : MonoBehaviour
{
    public Button[] foodButtons;

    // Start is called before the first frame update
    void Start()
    {
        foodButtons = GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
