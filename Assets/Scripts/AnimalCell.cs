using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalCell : MonoBehaviour, IPointerClickHandler
{
    GameManager gameManager;
    public string animalName;
    public int id;
    public int virusNum;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;

        if (transform.childCount != 0)
            animalName = transform.GetChild(0).gameObject.name;
    }

    public void OnPointerClick(PointerEventData ped)
    {
        //whatever happens on click

        //tells game manager which cell is clicked
        gameManager.OnAnimalCellClicked(id);
        Debug.Log("hit" + ", animal name: " + animalName);        
    }

    public void ShowAnimal(string n)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
