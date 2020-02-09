using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowText : MonoBehaviour
{
    Text text;
    public string content;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }


    private void OnEnable()
    {
        text.text = content;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
