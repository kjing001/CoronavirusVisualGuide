using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalCell : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnPointerClick(PointerEventData ped)
    {
        //whatever happens on click
        Debug.Log("hit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
