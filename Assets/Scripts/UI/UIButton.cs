using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public int id
    {
        get { return transform.GetSiblingIndex(); }
    }

    public Button button
    {
        get { return GetComponent<Button>(); }
    }

    public Image image
    {
        get
        {
            var img = GetComponent<Image>();
            if (img == null)
                img = GetComponentInChildren<Image>();
            return img;
        }        
    }

    public Text text
    {
        get
        {
            var txt = GetComponent<Text>();
            if (txt == null)
                txt = GetComponentInChildren<Text>();
            return txt;
        }

    }

}
