using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalCell : MonoBehaviour, IPointerClickHandler
{
    GameManager gameManager;
    public string animalName;
    public int id;
    public int virusCount;
    public int maxVirusCount;
    public List<string> infectionTargets = new List<string>();
    public GameObject number;
    public GameObject virusIcon;

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

    public void UpdateVirusSprite()
    {
        if (number == null || virusIcon == null)
            foreach (Transform item in transform)
            {
                if (item.name == "Number")            
                   number = item.gameObject;
                if (item.name == "Icon")
                    virusIcon = item.gameObject;
            }
        if (number != null)
            number.GetComponent<SpriteRenderer>().sprite = virusCount > 0 ?
                    gameManager.numberIcons[virusCount - 1] : null;    
        
        if(virusIcon != null)
            virusIcon.SetActive(virusCount > 0);
    }

    public void ShowAnimal(string n)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
