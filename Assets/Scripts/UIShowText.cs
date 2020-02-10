using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowText : MonoBehaviour
{
    Text text;
    public string content;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnEnable()
    {
        text = GetComponent<Text>();

        //text.text = content;
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
