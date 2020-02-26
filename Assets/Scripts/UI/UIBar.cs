using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    protected GameManager manager;
    public RectTransform back;
    public RectTransform front;
    protected float value;
    protected float maxValue;
    protected float length;
    protected float maxLength;
    protected float currentLenght;
    protected float height;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (back == null)
            back = GetComponent<RectTransform>();
        
        if (front == null)
            front = transform.GetChild(0).GetComponent<RectTransform>();

        height = back.sizeDelta.y;
        maxLength = back.sizeDelta.x;
        manager = GameManager.instance;
    }

    virtual protected void OnValueChange()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
